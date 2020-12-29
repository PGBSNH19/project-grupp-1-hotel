using Hotel.Server.Models;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories.Interfaces;
using Hotel.Shared;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Repositories
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        public BookingRepository(HotelContext ctx) : base(ctx)
        { }

        public IQueryable<int> GetUnavailableRoomIds(RoomAvailabilityRequest request)
        {
            Log.Information("BookingRepository processing request for GetUnavailableRoomIds {@Request}", request);
            var ids = ctx.Bookings
                .Where(e => !(request.CheckOutDate <= e.CheckInDate || request.CheckInDate >= e.CheckOutDate) && e.Room != null && !e.IsCanceled)
                .Include(e => e.Room)
                .Select(e => e.Room.Id);
            return ids;
        }

        public IQueryable<Room> GetAvailableRooms(IQueryable<int> unavailableIDs)
        {
            Log.Information("BookingRepository processing request for GetAvailableRooms {@IDs}", unavailableIDs);
            var query = ctx.Rooms
                .Where(r => !unavailableIDs.Contains(r.Id));
            return query;
        }

        public async Task<Booking> GetByBookingNumberAsync(string bookingnumber)
        {
            Log.Information("BookingRepository processing request for GetByBookingNumberAsync {@BookingNumber}", bookingnumber);
            var result = await ctx.Bookings.Include(e => e.Room).FirstOrDefaultAsync(e => e.BookingNumber == bookingnumber);
            return result;
        }
    }
}
