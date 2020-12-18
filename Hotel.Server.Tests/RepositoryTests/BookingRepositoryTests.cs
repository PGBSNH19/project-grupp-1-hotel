using Hotel.Server.Models;
using Hotel.Server.Models.Request;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hotel.Server.Tests.RepositoryTests
{
    public class BookingRepositoryTests
    {
        private static BookingRepository GetRepoMockSetup(bool empty = false)
        {
            var ctx = new Mock<HotelContext>();
            var bookings = MockData.MockBookings;
            var rooms = MockData.MockRooms;
            bookings[0].Room = new Room { Id = 1 };
            ctx.Setup(x => x.Bookings).ReturnsDbSet(empty ? new List<Booking>() : bookings);
            ctx.Setup(x => x.Rooms).ReturnsDbSet(empty ? new List<Room>() : rooms);
            return new BookingRepository(ctx.Object);
        }

        [Fact]
        public void GetUnavailableRooms_IfOnlyOneRoomUnavailable_ReturnId()
        {
            var repo = GetRepoMockSetup();

            var request = new RoomAvailabilityRequest // intersect bookings on at least one extreme
            {
                CheckInDate = DateTime.Parse("Dec 14, 2020"),
                CheckOutDate = DateTime.Parse("Dec 16, 2022")
            };
            var result = repo.GetUnavailableRoomIds(request);
            var res = result.ToArray();
            var expected = 1;

            Assert.True(res.Length == expected);
        }

        [Fact]
        public void GetAvailableRoomTypes_IfOnlyOneRoomUnavailable_ReturnOtherRooms()
        {
            var repo = GetRepoMockSetup();

            var request = new RoomAvailabilityRequest // intersect bookings on at least one extreme
            {
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(1)
            };
            var ids = repo.GetUnavailableRoomIds(request);
            var result = repo.GetAvailableRooms(ids);
            var res = result.ToArray();
            var expected = 2;

            Assert.True(res.Length == expected);
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        public async void GetByBookingNumberAsync_BookingsExists_ReturnExpectedObjects(string bookingNumber)
        {
            var repo = GetRepoMockSetup();

            var result = await repo.GetByBookingNumberAsync(bookingNumber);
            var expected = bookingNumber;

            Assert.True(result.BookingNumber == expected);
            Assert.IsType<Booking>(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("12jm9kd9")]
        [InlineData("#0)\")\")#=")]
        public async void GetByBookingNumberAsync_BadOrNonExistingParameters_ReturnsNull(string bookingNumber)
        {
            var repo = GetRepoMockSetup();

            var result = await repo.GetByBookingNumberAsync(bookingNumber);

            Assert.Null(result);
        }
    }
}
