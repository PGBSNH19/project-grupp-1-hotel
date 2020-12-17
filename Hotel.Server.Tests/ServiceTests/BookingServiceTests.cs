using Hotel.Server.Models;
using Hotel.Server.Models.Info;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories;
using Hotel.Server.Repositories.Interfaces;
using Hotel.Server.Services;
using Hotel.Server.Services.Communication;
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
        private static BookingService GetRepoMockSetup(List<Booking> bookings, List<Room> rooms)
        {
            var ctx = new Mock<HotelContext>();
            ctx.Setup(x => x.Rooms).ReturnsDbSet(rooms);
            ctx.Setup(x => x.Bookings).ReturnsDbSet(bookings);
            var bookingRepository = new BookingRepository(ctx.Object);
            return new BookingService(bookingRepository);
        }

        [Fact] public async void CreateAsync_IncomingBookingRequestPasses_ReturnsBooking()
        {
            var service = GetRepoMockSetup(new List<Booking>(), MockData.MockRooms);
            var bookingRequest = MockData.MockBookingRequest;

            var result = await service.CreateAsync(bookingRequest);

            Assert.IsType<ServiceResponse<BookingInfo>>(result);
        }

        [Fact]
        public async void CreateAsync_IncomingBookingRequestDoesNotPassDueToNoAvailableRooms_ReturnsNoSuccess()
        {
            var service = GetRepoMockSetup(new List<Booking>(), MockData.MockRooms);
            var bookingRequest = MockData.MockBookingRequest;

            var result = await service.CreateAsync(bookingRequest);

            Assert.IsType<ServiceResponse<BookingInfo>>(result);
        }


        [Fact]
        public async void GetAvailableRoomTypesAsync_IfAllRoomsIsAvailable_ReturnAllDistinctRoomTypes()
        {
            var service = GetRepoMockSetup(new List<Booking>(), MockData.MockRooms);

            var roomAvailablilityRequest = MockData.MockRoomAvailabilityRequest[1];

            var result = await service.GetAvailableRoomTypesAsync(roomAvailablilityRequest);
            var resultCount = result.Entity.Count;
            var expected = MockData.MockRooms.Distinct()
                .Select(s => s.ToDto())
                .ToArray().Length;

            Assert.Equal(expected, resultCount);
        }

        [Fact]
        public async void GetAvailableRoomTypesAsync_IfAllRoomsIsBooked_ReturnCountZero()
        {
            List<Room> rooms = new List<Room>
            {
               new Room{Id = 1, Beds = 1, DoubleBeds = 0, IsCondo = false, IsSuite = false, ImageUrl = "", Pets = false, Smoking = false },
               new Room{Id = 2, Beds = 2, DoubleBeds = 1, IsCondo = false, IsSuite = false, ImageUrl = "", Pets = false, Smoking = false }
            };
            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id = 1, BookingNumber = "foo",Room = rooms[0], CheckInDate = DateTime.Parse("Dec 15, 2020"), CheckOutDate = DateTime.Parse("Dec 20, 2020") },
                new Booking { Id = 2, BookingNumber = "foo",Room = rooms[1], CheckInDate = DateTime.Parse("Dec 15, 2020"), CheckOutDate = DateTime.Parse("Dec 20, 2020") }
            };

            var service = GetRepoMockSetup(bookings, rooms);


            var roomAvailablilityRequest = MockData.MockRoomAvailabilityRequest[0];

            var result = await service.GetAvailableRoomTypesAsync(roomAvailablilityRequest);
            var resultCount = result.Entity.Count;
            var expected = 0;

            Assert.Equal(expected, resultCount);
        }

        [Fact]
        public async void GetAvailableRoomTypesAsync_IfRoomsIsBooked_ReturnDistinctRooms()
        {
            List<Room> rooms = new List<Room>
            {
               new Room{Id = 1, Beds = 1, DoubleBeds = 0, IsCondo = false, IsSuite = false, ImageUrl = "", Pets = false, Smoking = false },
               new Room{Id = 2, Beds = 2, DoubleBeds = 1, IsCondo = false, IsSuite = false, ImageUrl = "", Pets = false, Smoking = false },
               new Room{Id = 3, Beds = 1, DoubleBeds = 1, IsCondo = false, IsSuite = false, ImageUrl = "", Pets = false, Smoking = false }
            };
            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id = 1, BookingNumber = "foo",Room = rooms[0], CheckInDate = DateTime.Parse("Dec 15, 2020"), CheckOutDate = DateTime.Parse("Dec 26, 2020") },
                new Booking { Id = 2, BookingNumber = "bar",Room = rooms[1], CheckInDate = DateTime.Parse("Dec 15, 2020"), CheckOutDate = DateTime.Parse("Dec 26, 2020") }
            };

            var service = GetRepoMockSetup(bookings, rooms);


            var roomAvailablilityRequest = MockData.MockRoomAvailabilityRequest[3];

            var result = await service.GetAvailableRoomTypesAsync(roomAvailablilityRequest);
            var expected = 1;

            Assert.Equal(expected, result.Entity.Count);
        }

        [Fact]
        public async void GetByBookingNumberAsync_BookingExists_ReturnBookingNumber()
        {
            List<Room> rooms = new List<Room>
            {
               new Room{Id = 1, Beds = 1, DoubleBeds = 0, IsCondo = false, IsSuite = false, ImageUrl = "", Pets = false, Smoking = false },
            };
            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id = 1, BookingNumber = "foo",Room = rooms[0], CheckInDate = DateTime.Parse("Dec 15, 2020"), CheckOutDate = DateTime.Parse("Dec 20, 2020") },
            };
            var service = GetRepoMockSetup(bookings, rooms);


            var result = await service.GetByBookingNumberAsync("foo");
            var expected = "foo";

            Assert.Equal(expected, result.BookingNumber);
        }

        [Fact]
        public async void GetByBookingNumberAsync_BookingNotExists_ReturnNull()
        {
            List<Room> rooms = new List<Room>
            {
               new Room{Id = 1, Beds = 1, DoubleBeds = 0, IsCondo = false, IsSuite = false, ImageUrl = "", Pets = false, Smoking = false },
            };
            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id = 1, BookingNumber = "foo",Room = rooms[0], CheckInDate = DateTime.Parse("Dec 15, 2020"), CheckOutDate = DateTime.Parse("Dec 20, 2020") },
            };
            var service = GetRepoMockSetup(bookings, rooms);


            var result = await service.GetByBookingNumberAsync("bar");

            Assert.Null(result);
        }
    }
}
