using Hotel.Server.Models.Request;
using Hotel.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace Hotel.Server.Controllers
{
    [ApiController]
    [Route("api/v1.0/[controller]")]
    public class BookingController : Controller
    {

        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("/{bookingNumber}")]
        public async Task<IActionResult> GetBookingByBookingNumber(string bookingNumber)
        {
            Log.Information("Controller method starting: [BookingController] GetBookingByBookingNumber");
            if (String.IsNullOrEmpty(bookingNumber)) 
                return BadRequest();

            var result = await _bookingService.GetByBookingNumberAsync(bookingNumber);

            if (result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpGet]
        [Route("/check/guests/{guests}/checkin/{checkIn}/checkout/{checkOut}")]
        public async Task<IActionResult> GetAvailableRooms(int guests, DateTime checkIn, DateTime checkOut)
        {
            Log.Information("Controller method starting: [BookingController] GetAvailableRooms");
            try
            {
                var roomAvailability = new RoomAvailabilityRequest
                {
                    Guests = guests,
                    CheckInDate = checkIn,
                    CheckOutDate = checkOut             
                };
                var result = await _bookingService.GetAvailableRoomTypesAsync(roomAvailability);
                if (result.Entity.Any())
                    return Ok(result.Entity);

                return NotFound(result.Message);
            }
            catch
            {
                return BadRequest();
            }            
        }
      
        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] BookingRequest bookingRequest)
        {
            Log.Information("Controller method starting: [BookingController] PostBooking");
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _bookingService.CreateAsync(bookingRequest);

            if(result.Entity != null)
                return Ok(result.Entity);

            return BadRequest(result.Message);
        }
    }
}
