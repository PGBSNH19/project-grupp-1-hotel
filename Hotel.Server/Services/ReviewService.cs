using Hotel.Server.Repositories.Interfaces;
using Hotel.Server.Services.Communication;
using Hotel.Server.Services.Interfaces;
using Hotel.Shared;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Log.Information("ReviewService processing request for GetAverageGradeAsync");
            return await Task.FromResult(_reviewRepository.GetAverageGradeAsync().Average(x => x.Grade));
        }
       

        public async Task<ServiceResponse<List<ReviewInfo>>> GetRandomReviewsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
