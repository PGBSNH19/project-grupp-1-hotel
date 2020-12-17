using Hotel.Server.Models.Request;
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
            if (String.IsNullOrEmpty(bookingNumber)) return BadRequest();


            return Ok();
        }

        [HttpGet]
        [Route("/check/")]
        public async Task<IActionResult> GetAvailableRooms([FromBody] RoomAvailabilityRequest roomAvailability)
        {
            if (roomAvailability == null) return BadRequest();


            return Ok();
        }
      
        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] BookingRequest bookingRequest)
        {
            if (bookingRequest == null) return BadRequest();

            return Ok();
        }
    }
}
