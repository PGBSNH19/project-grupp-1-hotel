using Hotel.Server.Controllers;
using Hotel.Server.Models;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories;
using Hotel.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Hotel.Server.Tests.ControllerTests
{
    public class ReviewControllerTests
    {
        private ReviewController GetRepoMockSetup(List<Review> reviews)
        {
            var ctx = new Mock<HotelContext>();
            ctx.Setup(x => x.Reviews).ReturnsDbSet(reviews);
            var reviewRepository = new ReviewRepository(ctx.Object);
            var service = new ReviewService(reviewRepository);

            return new ReviewController(service);
        }

        [Fact]
        public async void GetThreeRandomReviews_IfNoReviewsExists_ReturnNoContent()
        {
            var controller = GetRepoMockSetup(new List<Review>());

            var result = await controller.GetThreeRandomReviews();

            Assert.IsType<NotFoundObjectResult>(result);
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

            var controller = GetRepoMockSetup(reviews);

            var result = await controller.GetThreeRandomReviews();

            Assert.IsType<NotFoundObjectResult>(result);
        }




    }
}
