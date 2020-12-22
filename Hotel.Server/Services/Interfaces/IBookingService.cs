using Hotel.Shared;
using Hotel.Server.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Server.Services.Interfaces
{
    public interface IBookingService
    {
        Task<ServiceResponse<BookingInfo>> CreateAsync(BookingRequest request);
        Task<ServiceResponse<List<RoomInfo>>> GetAvailableRoomTypesAsync(RoomAvailabilityRequest request);
        Task<BookingInfo> GetByBookingNumberAsync(string bookingNumber);
    }
}
