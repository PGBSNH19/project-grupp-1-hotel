﻿using Hotel.Server.Services.Interfaces;
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
