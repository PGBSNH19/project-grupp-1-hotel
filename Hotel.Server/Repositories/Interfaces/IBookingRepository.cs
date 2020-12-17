using Hotel.Server.Models;
using Hotel.Server.Models.Info;
using Hotel.Server.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Repositories.Interfaces
{
    public interface IBookingRepository : IBaseRepository
    {
        IQueryable<int> GetUnavailableRoomIdsAsync(RoomAvailabilityRequest request);
        IQueryable<Room> GetAvailableRoomsAsync(IQueryable<int> unavailableIDs);
    }
}
