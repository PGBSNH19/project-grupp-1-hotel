using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        private readonly IConfiguration _config;

        public WeatherForecastController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("settings")]
        public ActionResult Settings()
        {
            return Ok(_config.GetConnectionString("DefaultConnection"));
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
            var d = new DummyInfo { Content = "Development server up and running" };
            return Ok(d);
        }

        public class DummyInfo
        {
            public string Content { get; set; }
        }
    }
}
