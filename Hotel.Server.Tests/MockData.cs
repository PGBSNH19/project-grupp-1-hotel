using Hotel.Server.Models;
using Hotel.Server.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Server.Tests
{
    public static class MockData
    {
        public static List<Booking> MockBookings => new List<Booking> {
                new Booking { Id = 1, BookingNumber = "foo", CheckInDate = DateTime.Parse("Dec 15, 2020"), CheckOutDate = DateTime.Parse("Dec 20, 2020") },
                new Booking { Id = 2, BookingNumber = "bar", CheckInDate = DateTime.Parse("Dec 15, 2020"), CheckOutDate = DateTime.Parse("Dec 20, 2020") },
            };

        public static List<Room> MockRooms => new List<Room> {
                new Room { Id = 1, Beds = 1, DoubleBeds = 1, IsCondo = false, IsSuite = false },
                new Room { Id = 2, Beds = 2, DoubleBeds = 1, IsCondo = true, IsSuite = false },
                new Room { Id = 3, Beds = 2, DoubleBeds = 0, IsCondo = false, IsSuite = false },
            };

        public static List<RoomAvailabilityRequest> MockRoomAvailabilityRequest => new List<RoomAvailabilityRequest>
        {
            new RoomAvailabilityRequest {CheckInDate = DateTime.Parse("Dec 17, 2020"), CheckOutDate = DateTime.Parse("Dec 18, 2020"), Guests = 1},
            new RoomAvailabilityRequest {CheckInDate = DateTime.Parse("Dec 20, 2020"), CheckOutDate = DateTime.Parse("Dec 24, 2020"), Guests = 3},
            new RoomAvailabilityRequest {CheckInDate = DateTime.Parse("Dec 20, 2020"), CheckOutDate = DateTime.Parse("Dec 24, 2020"), Guests = 2},
            new RoomAvailabilityRequest {CheckInDate = DateTime.Parse("Dec 20, 2020"), CheckOutDate = DateTime.Parse("Dec 24, 2020"), Guests = 1},
            new RoomAvailabilityRequest {CheckInDate = DateTime.Parse("Dec 22, 2020"), CheckOutDate = DateTime.Parse("Dec 26, 2020"), Guests = 3},
        };
    }
}
