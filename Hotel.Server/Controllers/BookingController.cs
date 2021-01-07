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
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        ///<summary>
        ///Retrieves a specific booking by unique bookingnumber
        ///</summary>
        ///<response code="200">Booking found with the specified bookingnumber</response>
        ///<response code="404">No booking was found with the specified bookingnumber</response>
        ///<response code="400">The specified bookingnumber was null or empty</response>
        [HttpGet]
        [ProducesResponseType(typeof(BookingInfo), 200)]
        [Route("{bookingNumber}")]
        public async Task<IActionResult> GetBookingByBookingNumber(string bookingNumber)
        {
            Log.Information("Controller method starting: [BookingController] GetBookingByBookingNumber");
            if (String.IsNullOrEmpty(bookingNumber))
                return BadRequest();

            var result = await _bookingService.GetByBookingNumberAsync(bookingNumber);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        ///<summary>
        ///Retrives available rooms by number of guests and between check in and check out dates 
        ///</summary>
        ///<response code="200">Returns array with rooms</response>
        ///<response code="404">No room/rooms avaible for customer</response>
        ///<response code="400">The input data was not correct, string properties checkIn and checkOut needs to be in DateTime.Parse format</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<RoomInfo>), 200)]
        [Route("check/guests/{guests}/checkin/{checkIn}/checkout/{checkOut}")]
        public async Task<IActionResult> GetAvailableRooms(int guests, string checkIn, string checkOut)
        {
            Log.Information("Controller method starting: [BookingController] GetAvailableRooms");
            try
            {
                var roomAvailability = new RoomAvailabilityRequest
                {
                    Guests = guests,
                    CheckInDate = DateTime.Parse(checkIn),
                    CheckOutDate = DateTime.Parse(checkOut)
                };
                var result = await _bookingService.GetAvailableRoomTypesAsync(roomAvailability);
                if (!result.Success)
                    return BadRequest();

                return Ok(result.Entity);
            }
            catch
            {
                return BadRequest();
            }
        }

        ///<summary>
        ///Posts booking to databse
        ///</summary>
        ///<response code="200">Post successful</response>
        ///<response code="404">Post Body was not valid or save to database failure, read return message for more information</response>
        [HttpPost]
        public async Task<IActionResult> PostBooking([FromBody] BookingRequest bookingRequest)
        {
            Log.Information("Controller method starting: [BookingController] PostBooking");
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _bookingService.CreateAsync(bookingRequest);

            if (result.Entity != null)
                return Ok(result.Entity);

            return BadRequest(result.Message);
        }

        ///<summary>
        ///Cancels a Booking with given BookingNumber and attached Emailaddress
        ///</summary>
        ///<response code="200">Returns canceled Booking</response>
        ///<response code="404">Found no booking with BookingNumber/Email does not match Booking</response>
        ///<response code="400">The input data was not correct, BookingNumber and Email cannot be empty or whitespace</response>
        [HttpPut]
        [Route("{bookingNumber}/cancel")]
        public async Task<IActionResult> CancelBooking([FromRoute]string bookingNumber, [FromBody]string email)
        {
            Log.Information("Controller method starting: [BookingController] CancelBooking");
            if (string.IsNullOrWhiteSpace(bookingNumber)) return BadRequest("BookingNumber not valid");
            if (string.IsNullOrWhiteSpace(email)) return BadRequest("Email not valid");

            var result = await _bookingService.CancelAsync(bookingNumber, email);
            if (!result.Success) 
                return NotFound(result.Message);

            return Ok(result.Entity);
        }
    }
}
