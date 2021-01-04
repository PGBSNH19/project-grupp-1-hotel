using Hotel.Server.Models;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories;
using Hotel.Server.Services;
using Hotel.Shared;
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
    public class ReviewServiceTests
    {
        private static ReviewService GetRepoMockSetup(List<Review> reviews, List<Booking> bookings)
        {
            var ctx = new Mock<HotelContext>();
            ctx.Setup(x => x.Reviews).ReturnsDbSet(reviews);
            ctx.Setup(x => x.Bookings).ReturnsDbSet(bookings);
            var reviewRepository = new ReviewRepository(ctx.Object);
            return new ReviewService(reviewRepository);
        }

        [Fact]
        public async void GetRandomReviews_ReturnsThreeReviewsAndEveryReviewHasGradeOf4OrHigher()
        {
            var service = GetRepoMockSetup(MockData.MockReviews, MockData.MockBookings);

            var result = await service.GetRandomReviewsAsync();

            var expected = 3;

            Assert.Equal(expected, result.Entity.Count);
            Assert.True(result.Entity[0].Grade >= 4);
            Assert.True(result.Entity[1].Grade >= 4);
            Assert.True(result.Entity[2].Grade >= 4);
        }

       [Fact]
       public async void GetAverageGradeAsync_ReturnTypeIsOfTypeDouble()
       {
            var service = GetRepoMockSetup(MockData.MockReviews, MockData.MockBookings);

            var result = await service.GetAverageGradeAsync();

            Assert.IsType<Double>(result);
       }

        [Theory]
        [InlineData("foo")]
        [InlineData("bar")]

        public async void CreateReviewAsync_IncomingBookingNumberExistInBookingTableButNotInReviewTable_ReturnsValidReviewInfo(string bookingnumber)
        {
            var service = GetRepoMockSetup(MockData.MockReviews, MockData.MockBookings);
            ReviewRequest reviewRequest = new ReviewRequest
            {
                Grade = 4,
                BookingNumber = bookingnumber
            };
            var result = await service.CreateReviewAsync(reviewRequest);

            Assert.IsType<ReviewInfo>(result.Entity);
        }

        [Theory]
        [InlineData("booking")]
        [InlineData("Booking2")]

        public async void CreateReviewAsync_IncomingBookingNumberDoesNotExistInBookingTable_ReturnReviewInfoEntityAsNull(string bookingnumber)
        {
            var service = GetRepoMockSetup(MockData.MockReviews, MockData.MockBookings);
            ReviewRequest reviewRequest = new ReviewRequest
            {
                Grade = 4,
                BookingNumber = bookingnumber
            };
            var result = await service.CreateReviewAsync(reviewRequest);

            Assert.Null(result.Entity);
        }

        [Theory]
        [InlineData("f10db40a-8906-4788-99c8-8292361be7c2")]
        public async void CreateReviewAsync_IncomingBookingNumberExistInReviewTable_ReturnReviewInfoEntityAsNull(string bookingnumber)
        {
            var service = GetRepoMockSetup(MockData.MockReviews, MockData.MockBookings);
            ReviewRequest reviewRequest = new ReviewRequest
            {
                Grade = 4,
                BookingNumber = bookingnumber
            };
            var result = await service.CreateReviewAsync(reviewRequest);

            Assert.Null(result.Entity);
        }

        [Fact]

        public async void CreateReviewAsync_IncomingReviewHasAnonymousAsTrue_ReturnReviewInfoEntityWithNameAsNullOrEmpty()
        {
            var service = GetRepoMockSetup(MockData.MockReviews, MockData.MockBookings);
            ReviewRequest reviewRequest = new ReviewRequest
            {
                Grade = 4,
                BookingNumber = "foo",
                Anonymous = true,
            };

            var result = await service.CreateReviewAsync(reviewRequest);

            Assert.True(String.IsNullOrEmpty(result.Entity.FirstName));
            Assert.True(String.IsNullOrEmpty(result.Entity.LastName));
        }

        [Fact]

        public async void CreateReviewAsync_IncomingReviewHasAnonymousAsFalse_ReturnReviewInfoEntityNameIsNotNullOrEmpty()
        {
            var service = GetRepoMockSetup(MockData.MockReviews, MockData.MockBookings);
            ReviewRequest reviewRequest = new ReviewRequest
            {
                Grade = 4,
                BookingNumber = "foo",
                Anonymous = false,
            };

            var result = await service.CreateReviewAsync(reviewRequest);

            Assert.False(String.IsNullOrEmpty(result.Entity.FirstName));
            Assert.False(String.IsNullOrEmpty(result.Entity.LastName));
        }

        [Fact]
        public async void CreateReviewAsync_IncomingReviewHasHasGradeAs0_ReturnReviewInfoEntityAsNull()
        {
            var service = GetRepoMockSetup(MockData.MockReviews, MockData.MockBookings);
            ReviewRequest reviewRequest = new ReviewRequest
            {
                Grade = 0,
                BookingNumber = "foo",
                Anonymous = false,
            };

            var result = await service.CreateReviewAsync(reviewRequest);

            Assert.Null(result.Entity);
        }


    }
}
