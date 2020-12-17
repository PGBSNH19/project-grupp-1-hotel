using Hotel.Server.Models;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories;
using Hotel.Server.Repositories.Interfaces;
using Hotel.Server.Services;
using Hotel.Server.Services.Interfaces;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotel.Server.Tests.ServiceTests
{
    public class BookingServiceTests
    {
        private static BookingService GetRepoMockSetup(bool empty = false)
        {
            var ctx = new Mock<HotelContext>();
            var rooms = MockData.MockRooms;
            ctx.Setup(x => x.Rooms).ReturnsDbSet(empty ? new List<Room>() : rooms);
            var bookingRepository = new BookingRepository(ctx.Object);
            return new BookingService(bookingRepository);
        }

        [Fact]
        public void GetAvailableRoomTypesAsync_IfNoRoomIsBooked_ReturnAllDistinctRoomTypes()
        {
            var service = GetRepoMockSetup();

            var roomAvailablilityRequest = MockData.MockRoomAvailabilityRequest[0];

            var result = service.GetAvailableRoomTypesAsync(roomAvailablilityRequest).Result.Entity.Length;
            var expected = MockData.MockRooms.Distinct()
                .Select(s => s.ToDto())
                .ToArray().Length;

            Assert.Equal(result, expected);

        }
    }
}
