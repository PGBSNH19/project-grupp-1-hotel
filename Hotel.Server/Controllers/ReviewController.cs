using Hotel.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        ///<summary>
        ///Returns three random reviews with grades greater or equal to 4
        ///</summary>
        ///<response code="200">Three random reviews was found and returned</response>
        ///<response code="400">Request failed, read error messages</response>
        [HttpGet]
        [Route("random")]
        public async Task<IActionResult> GetThreeRandomReviews()
        {
            var reviews = await _reviewService.GetRandomReviewsAsync();

            if(reviews == null)
            {
                return BadRequest();
            }

            return Ok(reviews.Entity);
        }
        
        ///<summary>
        ///Retrieves the average grade out of all reviews 
        ///</summary>
        ///<response code="200">Returns a double value</response>
        [HttpGet]
        [Route("average")]
        public async Task<IActionResult> GetAverage()
        {
            Log.Information("Controller method starting: [ReviewController] GetAverage");
            var result = await _reviewService.GetAverageGradeAsync();
            return Ok(result);
        }
    }
}
