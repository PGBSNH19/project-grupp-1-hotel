using Hotel.Server.Models;
using Hotel.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Repositories.Interfaces
{
    public interface IBookingRepository : IBaseRepository
    {
        IQueryable<int> GetUnavailableRoomIds(RoomAvailabilityRequest request);
        IQueryable<Room> GetAvailableRooms(IQueryable<int> unavailableIDs);
        Task<Booking> GetByBookingNumberAsync(string bookingnumber);
    }
}
