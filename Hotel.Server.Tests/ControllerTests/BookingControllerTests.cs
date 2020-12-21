using Hotel.Server.Controllers;
using Hotel.Server.Models;
using Hotel.Server.Models.Info;
using Hotel.Server.Models.Request;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories;
using Hotel.Server.Services;
using Hotel.Server.Services.Communication;
using Hotel.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hotel.Server.Tests.ControllerTests
{
    public class BookingControllerTests
    {
        private static BookingController GetRepoMockSetup(List<Booking> bookings, List<Room> rooms)
        {
            var ctx = new Mock<HotelContext>();
            ctx.Setup(x => x.Rooms).ReturnsDbSet(rooms);
            ctx.Setup(x => x.Bookings).ReturnsDbSet(bookings);
            var bookingRepository = new BookingRepository(ctx.Object);
            var service = new BookingService(bookingRepository);
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
        public async void GetBookingByBookingNumber_IncomingBookingNumberExist_RetunsOkResult(string bookingNumber)
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, MockData.MockRooms);

            var result = await controller.GetBookingByBookingNumber(bookingNumber);

            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("NoId")]
        [InlineData("NoId2")]
        public async void GetBookingByBookingNumber_IncomingBookingNumberDoesNotExist_RetunsNotFound(string bookingNumber)
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, MockData.MockRooms);

            var result = await controller.GetBookingByBookingNumber(bookingNumber);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void GetAvailableRooms_IncomingRequestsDatesAreNotValid_RetunsBadRequest()
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, MockData.MockRooms);

            var result = await controller.GetAvailableRooms(1, new DateTime(), DateTime.Parse("Dec 24, 2023"));

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
