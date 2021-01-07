using Hotel.Server.Services.Interfaces;
using Hotel.Shared;
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

            if(reviews.Entity != null)
                return Ok(reviews.Entity);

            return NotFound(reviews.Message);
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

        ///<summary>
        ///Post new review
        ///</summary>
        ///<response code="200">Post successful and saved in database</response>
        ///<response code="400">Post failed, possiblie outcomes: RequestBody not valid, the bookingnumber does not exsist, there is already a review posted
        ///with the posted bookingnumber or saved failed. Read error message</response>
        [HttpPost]
        public async Task<IActionResult> PostReview([FromBody] ReviewRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var postRequest = await _reviewService.CreateReviewAsync(request);

            if (postRequest.Entity == null)
                return BadRequest(postRequest.Message);

            return Ok(postRequest.Entity);
        }
    }
}
