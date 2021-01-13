using Hotel.Server.Services.Communication;
using Hotel.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hotel.Server.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ServiceResponse<List<ReviewInfo>>> GetRandomReviewsAsync();
        Task<double> GetAverageGradeAsync();
        Task<ServiceResponse<ReviewInfo>> CreateReviewAsync(ReviewRequest reviewRequest);
    }
}
