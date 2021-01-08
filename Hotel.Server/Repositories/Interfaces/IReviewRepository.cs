using Hotel.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Repositories.Interfaces
{
    public interface IReviewRepository : IBaseRepository, IBookingRepository
    {
        Task<List<Review>> GetThreeReviews();
        Task<Review[]> GetRandomReviewsAsync();
        IQueryable<Review> GetAverageGradeAsync();
        IQueryable<Review> GetReviewByBookingId(string bookingId);
    }
}
