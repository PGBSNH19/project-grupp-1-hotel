using Hotel.Server.Models;
using Hotel.Server.Models.Request;
using System;
using System.Collections.Generic;

namespace Hotel.Server.Tests
{
    public static class MockData
    {
        public static List<Booking> MockBookings => new List<Booking> {
                new Booking { Id = 1, BookingNumber = "foo", CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(5) },
                new Booking { Id = 2, BookingNumber = "bar", CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(5) },
            };

        public static List<Room> MockRooms => new List<Room> {
                new Room { Id = 1, Beds = 1, DoubleBeds = 1, IsCondo = false, IsSuite = false },
                new Room { Id = 2, Beds = 2, DoubleBeds = 1, IsCondo = true, IsSuite = false },
                new Room { Id = 3, Beds = 2, DoubleBeds = 0, IsCondo = false, IsSuite = false },
            };

        public static List<RoomAvailabilityRequest> MockRoomAvailabilityRequest => new List<RoomAvailabilityRequest>
        {
            new RoomAvailabilityRequest {CheckInDate = DateTime.Now.AddDays(2), CheckOutDate = DateTime.Now.AddDays(3), Guests = 1},
            new RoomAvailabilityRequest {CheckInDate = DateTime.Now.AddDays(5), CheckOutDate = DateTime.Now.AddDays(9), Guests = 3},
            new RoomAvailabilityRequest {CheckInDate = DateTime.Now.AddDays(5), CheckOutDate = DateTime.Now.AddDays(9), Guests = 2},
            new RoomAvailabilityRequest {CheckInDate = DateTime.Now.AddDays(5), CheckOutDate = DateTime.Now.AddDays(9), Guests = 1},
            new RoomAvailabilityRequest {CheckInDate = DateTime.Now.AddDays(7), CheckOutDate = DateTime.Now.AddDays(11), Guests = 3},
        };

        public static BookingRequest MockBookingRequest => new BookingRequest
        {
            Address = "Test Avenue 126", // booking info
            CheckInDate = DateTime.Now.AddDays(7),
            CheckOutDate = DateTime.Now.AddDays(11),
            Email = "test.testsson@gmail.com",
            FirstName = "Hotello",
            LastName = "Testovich",
            Guests = 1,
            PhoneNumber = "+46738429270",

            Beds = 1, // room info
            DoubleBeds = 1,
            SpaAccess = false,
            IsSuite = false,
            Breakfast = true
        };
    }
}
