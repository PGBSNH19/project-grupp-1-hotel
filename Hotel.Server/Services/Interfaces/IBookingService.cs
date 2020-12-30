using Hotel.Server.Services.Communication;
using Hotel.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Server.Services.Interfaces
{
    public interface IBookingService
    {
        Task<ServiceResponse<BookingInfo>> CreateAsync(BookingRequest request, bool isProduction = true);
        Task<ServiceResponse<List<RoomInfo>>> GetAvailableRoomTypesAsync(RoomAvailabilityRequest request);
        Task<BookingInfo> GetByBookingNumberAsync(string bookingNumber);
        Task<ServiceResponse<BookingInfo>> CancelAsync(string bookingNumber, string email);
    }
}
