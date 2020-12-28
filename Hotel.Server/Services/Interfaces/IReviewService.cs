using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Services.Interfaces
{
    public interface IReviewService
    {
        Task GetRandomReviewsAsync();
        Task GetAverageAsync();
        Task CreateReviewAsync();
    }
}
