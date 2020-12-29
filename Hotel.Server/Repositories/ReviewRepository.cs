﻿using Hotel.Server.Models;
using Hotel.Server.Persistence;
using Hotel.Server.Repositories.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Repositories
{
    public class ReviewRepository : BookingRepository, IReviewRepository
    {
        public ReviewRepository(HotelContext ctx) : base(ctx)
        { }

        public IQueryable<Review> GetAverageGradeAsync()
        {
            Log.Information("ReviewRepository processing request for GetAverageGradeAsync");
            return ctx.Reviews.Take(100);
        }

        public async Task<Review[]> GetRandomReviewsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
