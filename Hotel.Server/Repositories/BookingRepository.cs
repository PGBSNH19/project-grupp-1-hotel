using Hotel.Server.Models;
using Hotel.Server.Models.Info;
using Hotel.Server.Models.Request;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Repositories
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {

        public BookingRepository(HotelContext ctx) : base(ctx) 
        { }

        public  IQueryable<int> GetUnavailableRoomIdsAsync(RoomAvailabilityRequest request)
        {
            //var ids = await ctx.Bookings
            //    .Where(e => !(request.CheckOutDate <= e.CheckInDate || request.CheckInDate >= e.CheckOutDate)).Select(e => e.Id).ToArrayAsync();
            var ids = ctx.Bookings
                .Where(e => !(request.CheckOutDate <= e.CheckInDate || request.CheckInDate >= e.CheckOutDate) && e.Room != null)
                .Include(e => e.Room)
                .Select(e => e.Room.Id);

            return ids;
        }

        public IQueryable<Room> GetAvailableRoomsAsync(IQueryable<int> unavailableIDs)
        {
            var query = ctx.Rooms.Where(r => !unavailableIDs.Contains(r.Id));
            return query;
        }
    }
}
