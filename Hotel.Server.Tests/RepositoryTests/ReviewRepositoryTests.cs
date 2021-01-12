using Hotel.Server.Models;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hotel.Server.Tests.RepositoryTests
{
    public class ReviewRepositoryTests
    {
        private static ReviewRepository GetRepoMockSetup(bool empty = false)
        {
            var ctx = new Mock<HotelContext>();
            var reviews = MockData.MockReviews;
            ctx.Setup(x => x.Reviews).ReturnsDbSet(empty ? new List<Review>() : reviews);
            return new ReviewRepository(ctx.Object);
        }

        [Fact]
        public void GetReviewByBookingId_IfBookingIdExists_ReturnId()
        {
            var repo = GetRepoMockSetup();
            var bookingId = "f10db40a-8906-4788-99c8-8292361be7c2";

            var result = repo.GetReviewByBookingId(bookingId);

            Assert.Equal(result.FirstOrDefault().BookingNumber, bookingId);
        }

        [Fact]
        public void GetReviewByBookingId_IfBookingIdNotExists_ReturnNull()
        {
            var repo = GetRepoMockSetup();
            var bookingId = "foo";

            var result = repo.GetReviewByBookingId(bookingId);

            Assert.Null(result.FirstOrDefault());
        }

        [Fact]
        public void GetThreeReviews_ReviewsWithGradeOverFourExists_ReturnThreeReviews()
        {
            var repo = GetRepoMockSetup();
            var result = repo.GetThreeReviews();

            var expected = 3;

            Assert.Equal(result.Result.Count(), expected);
        }

        [Fact]
        public void GetThreeReviews_ReviewsWithGradeOverFourDoNotExists_ReturnCountZero()
        {
            var ctx = new Mock<HotelContext>();
            var reviews = MockData.MockReviews;
            reviews[0].Grade = 1;
            reviews[1].Grade = 1;
            reviews[2].Grade = 1;
            ctx.Setup(x => x.Reviews).ReturnsDbSet(reviews);
            var repo = new ReviewRepository(ctx.Object);

            var result = repo.GetThreeReviews();
            var expected = 0;


            Assert.Equal(result.Result.Count(), expected);
        }


    }
}
