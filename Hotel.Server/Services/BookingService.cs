using Hotel.Server.Models.Info;
using Hotel.Server.Models.Request;
using Hotel.Server.Services.Communication;
using Hotel.Server.Services.Interfaces;
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

        public Task<RoomInfo[]> GetAvailableRoomsAsync(RoomAvailabilityRequest request)
        {
            var unavailablerooms = await repo.GetUnavailableRooms(request);
            var rooms = await repo.GetAvailableRoomTypesAsync(unavailablerooms);
            return rooms;
        }

        public async Task<BookingInfo> GetByBookingNumberAsync(string bookingNumber)
        {
            var booking = await repo.GetByBookingNumberAsync(bookingNumber);
            var result = booking.ToDto();
            return result;
        }
    }
}
