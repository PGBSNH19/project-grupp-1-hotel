using Hotel.Server.Repositories.Interfaces;
using Hotel.Server.Services.Communication;
using Hotel.Server.Services.Interfaces;
using Hotel.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using System.Threading.Tasks;

namespace Hotel.Server.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository repo) => _reviewRepository = repo;

        public async Task<ServiceResponse<ReviewInfo>> CreateReviewAsync(ReviewRequest reviewRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<double> GetAverageGradeAsync()
        {
            throw new NotImplementedException();
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
                    reviews.Add(new ReviewInfo 
                    { 
                        Created = review.Created, 
                        Description = review.Description, 
                        FirstName = review.FirstName, 
                        LastName = review.LastName, 
                        Grade = review.Grade 
                    });
                }
                return new ServiceResponse<List<ReviewInfo>>(reviews);
            }
            return null;
        }
    }
}
