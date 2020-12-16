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
            throw new NotImplementedException();
        }

        public Task<RoomInfo[]> GetAvailableRoomsAsync(RoomAvailabilityRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<BookingInfo> GetByBookingNumberAsync(string bookingNumber)
        {
            var result = await repo.GetByBookingNumberAsync(bookingNumber);
            return result;
        }
    }
}
