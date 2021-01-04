using Hotel.Server.Controllers;
using Hotel.Server.Models;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories;
using Hotel.Server.Services;
using Hotel.Server.Services.Interfaces;
using Hotel.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hotel.Server.Tests.ControllerTests
{
    public class BookingControllerTests
    {
        private BookingController GetRepoMockSetup(List<Booking> bookings, List<Room> rooms)
        {
            var ctx = new Mock<HotelContext>();
            var emailService = new Mock<IEmailService>();
            ctx.Setup(x => x.Rooms).ReturnsDbSet(rooms);
            ctx.Setup(x => x.Bookings).ReturnsDbSet(bookings);
            var bookingRepository = new BookingRepository(ctx.Object);
            var service = new BookingService(bookingRepository, emailService.Object);

            return new BookingController(service);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async void GetBookingByBookingNumber_IncomingBookingNumberIsNullOrEmpty_ReturnsBadRequest(string bookingNumber)
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, MockData.MockRooms);

            var result = await controller.GetBookingByBookingNumber(bookingNumber);

            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]
        public async void GetBookingByBookingNumber_IncomingBookingNumberExist_ReturnsOkResult(string bookingNumber)
        {
            var rooms = MockData.MockRooms;
            var bookings = MockData.MockBookings;
            bookings[0].Room = rooms[0];
            bookings[1].Room = rooms[1];
            var controller = GetRepoMockSetup(bookings, rooms);

            var result = await controller.GetBookingByBookingNumber(bookingNumber);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("NoId")]
        [InlineData("NoId2")]
        public async void GetBookingByBookingNumber_IncomingBookingNumberDoesNotExist_ReturnsNotFound(string bookingNumber)
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, MockData.MockRooms);

            var result = await controller.GetBookingByBookingNumber(bookingNumber);

            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1, "2012-01-03", "aee")]
        [InlineData(1, "Lolipop", "2012-03-05")]
        [InlineData(3, "WrongDate", "wrongdate")]
        public async void GetAvailableRooms_IncomingRequestsDatesAreNotValid_ReturnsBadRequest(int guests, string checkinDate, string checkoutDate)
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, MockData.MockRooms);

            var result = await controller.GetAvailableRooms(guests, checkinDate, checkoutDate);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void PostBooking_IncomingBookingObjectIsValid_ReturnsOkObjectResult()
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, MockData.MockRooms);

            var result = await controller.PostBooking(MockData.MockBookingRequest);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void PostBooking_IncomingBookingObjectIsEmpty_ReturnsBadRequest()
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, MockData.MockRooms);

            var bookingRequest = new BookingRequest();

            var result = await controller.PostBooking(bookingRequest);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
