using Hotel.Server.Models;
using Hotel.Server.Models.Info;
using Hotel.Server.Models.Request;
using Hotel.Server.Repositories.Interfaces;
using Hotel.Server.Services.Communication;
using Hotel.Server.Services.Interfaces;
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

        public async Task<ServiceResponse<BookingInfo>> CreateAsync(BookingRequest request)
        {
            Log.Information("BookingService processing request for CreateAsync {@request}", request);

            if (request.CheckInDate < DateTime.Now)
                return new ServiceResponse<BookingInfo>("Check in date cannot be set earlier than today.");
            if (request.CheckOutDate < DateTime.Now)
                return new ServiceResponse<BookingInfo>("Check out date cannot be set earlier than today.");

            var query = repo.GetUnavailableRoomIds(new RoomAvailabilityRequest { 
                CheckInDate = request.CheckInDate, 
                CheckOutDate = request.CheckOutDate 
            });
            var availableroom = repo.GetAvailableRooms(query)
                .Where(e => e.Beds == request.Beds && e.DoubleBeds == request.DoubleBeds)
                .FirstOrDefault();
            if (availableroom == null) 
                return new ServiceResponse<BookingInfo>("Found no room with requested specifications.");

            var entity = request.ToDomain();
            entity.BookingNumber = Guid.NewGuid().ToString();
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

        public async Task<ServiceResponse<RoomInfo[]>> GetAvailableRoomTypesAsync(RoomAvailabilityRequest request)
        {
            Log.Information("BookingService processing request for GetAvailableRoomTypes {@request}", request);
            var unavailablequery = repo.GetUnavailableRoomIds(request);
            var roomTypes = await repo.GetAvailableRooms(unavailablequery)
                .Distinct()
                .Select(s => s.ToDto())
                .ToArrayAsync();
            return new ServiceResponse<RoomInfo[]>(roomTypes);
        }

        public async Task<BookingInfo> GetByBookingNumberAsync(string bookingNumber)
        {
            Log.Information("BookingService processing request for GetAvailableRoomTypes {@bookingNumber}", bookingNumber);
            var booking = await repo.GetByBookingNumberAsync(bookingNumber);
            var result = booking.ToDto();
            return result;
        }
    }
}
