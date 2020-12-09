using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hotel.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly JsonSerializerOptions defaultOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true, // When writing JSON
            PropertyNameCaseInsensitive = true, // When parsing JSON
        };

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("foo")]
        public ActionResult GetFoo()
        {
            return Ok();
        }

        [HttpGet]
        [Route("hello")]
        public async Task<ActionResult> GetHello()
        {
            var d = new DummyInfo { Content = "hello" };
            return Ok(d);
        }

        public class DummyInfo
        {
            public string Content { get; set; }
        }
    }
}
