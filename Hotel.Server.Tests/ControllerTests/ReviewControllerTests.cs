using Hotel.Server.Controllers;
using Hotel.Server.Models;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories;
using Hotel.Server.Services;
using Hotel.Shared;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace Hotel.Server.Tests.ControllerTests
{
    public class ReviewControllerTests
    {
        private ReviewController GetRepoMockSetup(List<Booking> bookings, List<Review> reviews)
        {
            var ctx = new Mock<HotelContext>();
            ctx.Setup(x => x.Reviews).ReturnsDbSet(reviews);
            ctx.Setup(x => x.Bookings).ReturnsDbSet(bookings);
            var reviewRepository = new ReviewRepository(ctx.Object);
            var bookingRepository = new BookingRepository(ctx.Object);
            var service = new ReviewService(reviewRepository);

            return new ReviewController(service);
        }

        [Fact]
        public async void GetThreeRandomReviews_IfNoReviewsExists_ReturnNoContent()
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, new List<Review>());

            var result = await controller.GetThreeRandomReviews();

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async void GetThreeRandomReviews_IfReviewsWithGradeFourOrHigherExists_ReturnOkObjectResult()
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, MockData.MockReviews);

            var result = await controller.GetThreeRandomReviews();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetThreeRandomReviews_IfNoReviewsWithGradeFourOrHigherExists_ReturnNoContent()
        {
            List<Review> reviews = new List<Review>
            {
                new Review
                {
                    Id = 1,
                    Description = "The most awesome hotel that I have ever visited, heads up to receptionist for being kind!",
                    Grade = 2,
                    Anonymous = false,
                    FirstName = "Hasse",
                    LastName = "Tagesson",
                    BookingNumber = "f10db40a-8906-4788-99c8-8292361be7c2"
                }
            };

            var controller = GetRepoMockSetup(MockData.MockBookings, reviews);

            var result = await controller.GetThreeRandomReviews();

            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async void GetAverage_IncomingReviewIsValid_GetAverageReviews()
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, MockData.MockReviews);

            var result = await controller.GetAverage();

            Assert.IsType<OkObjectResult>(result);
        }



        [Fact]
        public async void PostBooking_IncomingBookingObjectIsValid_ReturnsOkObjectResult()
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, new List<Review>());

            var result = await controller.PostReview(
                new ReviewRequest
                {
                    Description = "Friendly staff and extraordinary food in the restaurant.",
                    Grade = 5,
                    Anonymous = false,
                    BookingNumber = "foo"
                }
            );

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void PostBooking_IncomingBookingObjectIsValid_ReturnsNotFoundObjectResult()
        {
            var controller = GetRepoMockSetup(MockData.MockBookings, new List<Review>());

            var result = await controller.PostReview(
                new ReviewRequest
                {
                    Description = "Friendly staff and extraordinary food in the restaurant.",
                    Grade = 5,
                    Anonymous = false,
                    BookingNumber = ""
                }
            );

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
