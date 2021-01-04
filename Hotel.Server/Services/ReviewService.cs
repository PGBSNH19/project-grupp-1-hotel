using Hotel.Server.Repositories.Interfaces;
using Hotel.Server.Services.Communication;
using Hotel.Server.Services.Interfaces;
using Hotel.Shared;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Server.Extensions;

namespace Hotel.Server.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository repo) => _reviewRepository = repo;

        public async Task<ServiceResponse<ReviewInfo>> CreateReviewAsync(ReviewRequest reviewRequest)
        {
            Log.Information("ReviewService processing request for CreateAsync {@reviewRequest}", reviewRequest);

            var bookingNumberControl = await _reviewRepository.GetByBookingNumberAsync(reviewRequest.BookingNumber);

            if (bookingNumberControl == null)
                return new ServiceResponse<ReviewInfo>($"The bookingnumber does not exist");

            if (_reviewRepository.GetReviewByBookingId(reviewRequest.BookingNumber).Any())
                return new ServiceResponse<ReviewInfo>($"There is already a review posted with bookingnumber {reviewRequest.BookingNumber}");

            var entity = reviewRequest.ToDomain();

            if (!entity.Anonymous)
            {
                entity.FirstName = bookingNumberControl.FirstName;
                entity.LastName = bookingNumberControl.LastName;
            }

            try
            {
                await _reviewRepository.AddAsync(entity);
                await _reviewRepository.Complete();
            }
            catch (Exception ex)
            {
                Log.Error("Could not create new Review {@Message}", ex.Message);
                return new ServiceResponse<ReviewInfo>($"Could not create new Review: {ex.Message}");
            }
            return new ServiceResponse<ReviewInfo>(entity.ToDto());
        }

        public async Task<double> GetAverageGradeAsync()
        {
            Log.Information("ReviewService processing request for GetAverageGradeAsync");
            return await Task.FromResult(_reviewRepository.GetAverageGradeAsync().Average(x => x.Grade));
        }
       

        public async Task<ServiceResponse<List<ReviewInfo>>> GetRandomReviewsAsync()
        {
            Log.Information("ReviewService processing request for GetRandomReviewsAsync");
            var request = await _reviewRepository.GetThreeReviews();

            if (request.Any())
            {
                List<ReviewInfo> reviews = new List<ReviewInfo>();
                foreach (var review in request)
                {
                    reviews.Add(ModelExtensions.ToDto(review));
                }

                return new ServiceResponse<List<ReviewInfo>>(reviews);
            }
            return new ServiceResponse<List<ReviewInfo>>("No reviews found.");
        }
    }
}
