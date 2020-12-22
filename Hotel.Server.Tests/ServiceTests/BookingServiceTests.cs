using Hotel.Server.Extensions;
using Hotel.Server.Models;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories;
using Hotel.Server.Services;
using Hotel.Server.Services.Communication;
using Hotel.Shared;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public async void CreateAsync_IncomingBookingRequestPasses_ReturnsBooking()
        {
            var service = GetRepoMockSetup(new List<Booking>(), MockData.MockRooms);
            var bookingRequest = MockData.MockBookingRequest;

            var result = await service.CreateAsync(bookingRequest);

            Assert.IsType<ServiceResponse<BookingInfo>>(result);
        }

        [Fact]
        public async void CreateAsync_IncomingBookingRequestDoesNotPassDueToNoAvailableRooms_ReturnsNoSuccess()
        {
            var service = GetRepoMockSetup(new List<Booking>(), new List<Room>());
            var bookingRequest = MockData.MockBookingRequest;

            var result = await service.CreateAsync(bookingRequest);

            Assert.IsType<ServiceResponse<BookingInfo>>(result);
            Assert.True(result.Success == false);
        }

        [Fact]
        public async void GetAvailableRoomTypesAsync_IfAllRoomsAreAvailable_ReturnAllDistinctRoomTypes()
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
        public async void GetAvailableRoomTypesAsync_IfAllRoomsAreBooked_ReturnNoRooms()
        {
            var rooms = MockData.MockRooms;
            _ = rooms.PopLast();
            var bookings = MockData.MockBookings;
            bookings[0].Room = rooms[0];
            bookings[1].Room = rooms[1];
            var service = GetRepoMockSetup(bookings, rooms);
            var roomAvailablilityRequest = MockData.MockRoomAvailabilityRequest[0];

            var result = await service.GetAvailableRoomTypesAsync(roomAvailablilityRequest);
            var expected = 0;

            Assert.True(result.Entity.Count == expected);
            Assert.IsType<List<RoomInfo>>(result.Entity);
        }

        [Fact]
        public async void GetAvailableRoomTypesAsync_EarlierBookedRoomsHasNoImpact_ReturnDistinctRooms()
        {
            var rooms = MockData.MockRooms;
            var bookings = MockData.MockBookings;
            bookings[0].Room = rooms[0];
            bookings[1].Room = rooms[1];
            var service = GetRepoMockSetup(bookings, rooms);
            var roomAvailablilityRequest = MockData.MockRoomAvailabilityRequest[3];

            var result = await service.GetAvailableRoomTypesAsync(roomAvailablilityRequest);
            var expected = 3;

            Assert.Equal(expected, result.Entity.Count);
            Assert.IsType<List<RoomInfo>>(result.Entity);
        }

        [Fact]
        public async void GetAvailableRoomTypesAsync_CheckOutDateBeforeCheckinDate_ReturnNoSuccess()
        {
            var rooms = MockData.MockRooms;
            var bookings = MockData.MockBookings;
            bookings[0].Room = rooms[0];
            bookings[1].Room = rooms[1];
            var service = GetRepoMockSetup(bookings, rooms);
            var roomAvailablilityRequest = MockData.MockRoomAvailabilityRequest[1];
            roomAvailablilityRequest.CheckInDate = DateTime.Parse("Dec 13, 2022");
            roomAvailablilityRequest.CheckOutDate = DateTime.Parse("Dec 12, 2022");

            var result = await service.GetAvailableRoomTypesAsync(roomAvailablilityRequest);

            Assert.True(result.Success == false);
        }

        [Fact]
        public async void GetAvailableRoomTypesAsync_CheckOutDateSameDateAsCheckinDate_ReturnNoSuccess()
        {
            var rooms = MockData.MockRooms;
            var bookings = MockData.MockBookings;
            bookings[0].Room = rooms[0];
            bookings[1].Room = rooms[1];
            var service = GetRepoMockSetup(bookings, rooms);
            var roomAvailablilityRequest = MockData.MockRoomAvailabilityRequest[1];
            roomAvailablilityRequest.CheckInDate = DateTime.Now;
            roomAvailablilityRequest.CheckOutDate = DateTime.Now;

            var result = await service.GetAvailableRoomTypesAsync(roomAvailablilityRequest);

            Assert.True(result.Success == false);
        }

        [Fact]
        public async void GetAvailableRoomTypesAsync_CheckInDateEarlierThanToday_ReturnNoSuccess()
        {
            var rooms = MockData.MockRooms;
            var bookings = MockData.MockBookings;
            bookings[0].Room = rooms[0];
            bookings[1].Room = rooms[1];
            var service = GetRepoMockSetup(bookings, rooms);
            var roomAvailablilityRequest = MockData.MockRoomAvailabilityRequest[1];
            roomAvailablilityRequest.CheckInDate = DateTime.Now.AddDays(-1);
            roomAvailablilityRequest.CheckOutDate = DateTime.Now.AddDays(2);

            var result = await service.GetAvailableRoomTypesAsync(roomAvailablilityRequest);

            Assert.True(result.Success == false);
        }

        [Fact]
        public async void GetAvailableRoomTypesAsync_CheckOutDateEarlierThanToday_ReturnNoSuccess()
        {
            var rooms = MockData.MockRooms;
            var bookings = MockData.MockBookings;
            bookings[0].Room = rooms[0];
            bookings[1].Room = rooms[1];
            var service = GetRepoMockSetup(bookings, rooms);
            var roomAvailablilityRequest = MockData.MockRoomAvailabilityRequest[1];
            roomAvailablilityRequest.CheckInDate = DateTime.Now;
            roomAvailablilityRequest.CheckOutDate = DateTime.Now.AddDays(-1);

            var result = await service.GetAvailableRoomTypesAsync(roomAvailablilityRequest);

            Assert.True(result.Success == false);
        }

        [Fact]
        public async void GetByBookingNumberAsync_BookingExists_ReturnBookingNumber()
        {
            var rooms = MockData.MockRooms.PopLast().PopLast();
            var bookings = MockData.MockBookings.PopLast();
            bookings.First().Room = rooms.First();
            var service = GetRepoMockSetup(bookings, rooms);

            var result = await service.GetByBookingNumberAsync("foo");
            var expected = "foo";

            Assert.Equal(expected, result.BookingNumber);
        }

        [Fact]
        public async void GetByBookingNumberAsync_BookingNotExists_ReturnNull()
        {
            var rooms = new List<Room>();
            var bookings = new List<Booking>();
            var service = GetRepoMockSetup(bookings, rooms);

            var result = await service.GetByBookingNumberAsync("bar");

            Assert.Null(result);
        }
    }
}
