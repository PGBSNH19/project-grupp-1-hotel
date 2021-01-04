using Hotel.Server.Extensions;
using Hotel.Server.Repositories.Interfaces;
using Hotel.Server.Services.Communication;
using Hotel.Server.Services.Interfaces;
using Hotel.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Server.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository repo;

        private readonly IConfiguration _configuration;

        public BookingService(IBookingRepository repo, IConfiguration configuration)
        {
            this.repo = repo;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<BookingInfo>> CancelAsync(string bookingNumber, string email)
        {
            Log.Information("BookingService processing request for CancelAsync {@request}", bookingNumber);

            var booking = await repo.GetByBookingNumberAsync(bookingNumber);
            if (booking == null) return new ServiceResponse<BookingInfo>("Could not find any booking with given BookingNumber");
            if (booking.Email != email) return new ServiceResponse<BookingInfo>("Email does not match Booking");

            try
            {
                booking.Cancel();
                await repo.Complete();
                return new ServiceResponse<BookingInfo>(booking.ToDto());
            } catch(Exception ex) { return new ServiceResponse<BookingInfo>($"Failure canceling Booking: {ex.Message}"); }
        }

        public async Task<ServiceResponse<BookingInfo>> CreateAsync(BookingRequest request, bool isProduction = true)
        {
            Log.Information("BookingService processing request for CreateAsync {@request}", request);

            if (request.CheckInDate.Date < DateTime.Now.Date)
                return new ServiceResponse<BookingInfo>("Check in date cannot be set earlier than today.");
            if (request.CheckOutDate.Date <= DateTime.Now.Date)
                return new ServiceResponse<BookingInfo>("Check out date cannot be set earlier than today.");
            if (request.CheckInDate.Date >= request.CheckOutDate.Date)
                return new ServiceResponse<BookingInfo>("Check out date cannot occur before or same date as Check in date.");

            var query = repo.GetUnavailableRoomIds(new RoomAvailabilityRequest
            {
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate
            });

            var availableroom = repo.GetAvailableRooms(query)
                .Where(e => e.Beds == request.Beds && e.DoubleBeds == request.DoubleBeds).FirstOrDefault();
            if (availableroom == null)
                return new ServiceResponse<BookingInfo>("Found no room with requested specifications.");

            var entity = request.ToDomain();
            entity.Room = availableroom;

            try
            {
                await repo.AddAsync(entity);
                await repo.Complete();

                if (isProduction)
                {               
                    try
                    {
                        string checkin = entity.CheckInDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                        string checkout = entity.CheckOutDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                        var gmailPass = _configuration.GetSection("MailPassword").Value;
                        string breakfastIncluded = "";
                        string spaIncluded = "";

                        if (entity.Breakfast)
                        {
                            breakfastIncluded = $"Breakfast - Included<br>";
                        }
                        if (entity.SpaAccess)
                        {
                            spaIncluded = $"Spa Access - Included<br>";
                        }

                        StringBuilder mailbody = new StringBuilder();
                        mailbody.Append($"" +
                            $"<div style='background: #eee; margin: 20px; padding: 20px;'>" +
                            $"<center><h1>Hotell </h1><br>" +
                            $"<h4> We look forward to your stay.</h4>" +
                            $"<font> Check in is between 12:00 - 4:00 PM.<br>" +
                            $"Checkout before 12:00 PM on your last day.<br>" +
                            $"You have booked a room between the dates {checkin}" +
                            $"- {checkout}.<br></font><br><br>" +
                            $"<b>Booking No<b>: {entity.BookingNumber} <br><br></center>" +
                            $"<font><b> Your booking details:</b><br><br>" +
                            $"Number of guests - {entity.Guests}<br>" +
                            $"Check In - {checkin}<br>" +
                            $"Check Out - {checkout}<br>" +
                            $"{spaIncluded}" +
                            $"{breakfastIncluded}</font></div>");

                        MailMessage message = new MailMessage();
                        message.To.Add(entity.Email);
                        message.From = new MailAddress("hotellgruppett@gmail.com", "Hotell Group");
                        message.Subject = $"Booking Details for {entity.FirstName} {entity.LastName}";
                        message.Body = mailbody.ToString();
                        message.IsBodyHtml = true;
                        using (var smtp = new SmtpClient())
                        {
                            var credential = new NetworkCredential
                            {
                                UserName = "hotellgruppett@gmail.com",
                                Password = gmailPass  
                            };
                            smtp.Credentials = credential;
                            smtp.Host = "smtp.gmail.com";
                            smtp.Port = 587;
                            smtp.EnableSsl = true;
                            smtp.Send(message);
                        }
                    }
                    catch (Exception e)
                    {
                        string msg = "Mail cannot be sent";
                        msg += e.Message;
                        Log.Debug("Error: Inside catch block of Mail sending");
                        Log.Error("Error msg:" + e);
                        Log.Error("Stack trace:" + e.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Could not create new Booking {@Message}", ex.Message);
                return new ServiceResponse<BookingInfo>($"Could not create new Booking: {ex.Message}");
            }

            return new ServiceResponse<BookingInfo>(entity.ToDto());
        }

        public async Task<ServiceResponse<List<RoomInfo>>> GetAvailableRoomTypesAsync(RoomAvailabilityRequest request)
        {
            Log.Information("BookingService processing request for GetAvailableRoomTypes {@request}", request);

            if (request.CheckInDate.Date < DateTime.Now.Date)
                return new ServiceResponse<List<RoomInfo>>("Check in date cannot be set earlier than today.");
            if (request.CheckOutDate.Date <= DateTime.Now.Date)
                return new ServiceResponse<List<RoomInfo>>("Check out date cannot be set earlier than today.");
            if (request.CheckInDate.Date >= request.CheckOutDate.Date)
                return new ServiceResponse<List<RoomInfo>>("Check out date cannot occur before or same date as Check in date.");

            var unavailablequery = repo.GetUnavailableRoomIds(request);
            var roomTypes = await repo.GetAvailableRooms(unavailablequery)
                .Select(s => new { Beds = s.Beds, DoubleBeds = s.DoubleBeds })
                .Distinct()
                .ToArrayAsync();

            List<RoomInfo> roomtypes = new List<RoomInfo>(); // map the objects
            foreach (var roomtype in roomTypes)
                roomtypes.Add(new RoomInfo { Beds = roomtype.Beds, DoubleBeds = roomtype.DoubleBeds });

            return new ServiceResponse<List<RoomInfo>>(roomtypes);
        }

        public async Task<BookingInfo> GetByBookingNumberAsync(string bookingNumber)
        {
            Log.Information("BookingService processing request for GetAvailableRoomTypes {@bookingNumber}", bookingNumber);

            var booking = await repo.GetByBookingNumberAsync(bookingNumber);
            if (booking == null) return null;

            return booking.ToDto();
        }
    }
}
