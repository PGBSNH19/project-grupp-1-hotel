using Hotel.Server.Models;
using Hotel.Server.Models.Info;
using Hotel.Server.Models.Request;
using Hotel.Server.Repositories.Interfaces;
using Hotel.Server.Services.Communication;
using Hotel.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public Task<ServiceResponse<BookingInfo>> CreateAsync(BookingRequest request)
        {

        }

        public async Task<RoomInfo[]> GetAvailableRoomTypesAsync(RoomAvailabilityRequest request)
        {
            var unavailablequery = repo.GetUnavailableRoomIds(request);
            var roomTypes = await repo.GetAvailableRooms(unavailablequery)
                .Select(s => s.ToDto())
                .ToArrayAsync();
            
            return roomTypes;
        }

        public async Task<BookingInfo> GetByBookingNumberAsync(string bookingNumber)
        {
            var booking = await repo.GetByBookingNumberAsync(bookingNumber);
            var result = booking.ToDto();
            return result;
        }
    }
}
