using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class BookingController : Controller
    {
       
        [HttpGet]
        [Route("/{bookingNumber}")]
        public async Task<IActionResult> GetBookingByBookingNumber(string bookingNumber)
        {
            return Ok();
        }

        [HttpGet]
        [Route("/check/")]
        public async Task<IActionResult> GetAvailableRooms()
        {
            return Ok();
        }
      
        [HttpPost]
        public async Task<IActionResult> PostBooking()
        {
            return Ok();
        }

        [HttpGet]
        public ActionResult GetFoo()
        {
            return Ok();
        }
    }
}
