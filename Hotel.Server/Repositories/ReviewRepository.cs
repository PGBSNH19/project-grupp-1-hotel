using Hotel.Server.Persistence;
using Hotel.Server.Repositories.Interfaces;
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
    }
}
