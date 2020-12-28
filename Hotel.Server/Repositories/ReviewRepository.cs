using Hotel.Server.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Repositories
{
    public class ReviewRepository : BaseRepository
    {
        public ReviewRepository(HotelContext ctx) : base(ctx)
        {

        }
    }
}
