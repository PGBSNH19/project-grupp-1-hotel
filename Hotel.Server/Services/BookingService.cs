using Hotel.Server.Extensions;
using Hotel.Server.Repositories.Interfaces;
using Hotel.Server.Services.Communication;
using Hotel.Server.Services.Interfaces;
using Hotel.Shared;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository repo;

        public BookingService(IBookingRepository repo) => this.repo = repo;

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

        public async Task<ServiceResponse<BookingInfo>> CreateAsync(BookingRequest request)
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
