using Hotel.Server.Models;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Repositories
{
    public class ReviewRepository : BookingRepository, IReviewRepository
    {
        public ReviewRepository(HotelContext ctx) : base(ctx)
        {

        }
   
        public async Task<List<Review>> GetThreeReviews()
        {
            var reviews = await ctx.Reviews.Where(r => r.Grade >= 4).OrderBy(r => Guid.NewGuid()).Take(3).ToListAsync();
            return reviews;
        }
    }
}
