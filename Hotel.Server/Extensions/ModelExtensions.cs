using Hotel.Server.Models;
using Hotel.Shared;

namespace Hotel.Server.Extensions
{
    public static class ModelExtensions
    {
        public static Review ToDomain(this ReviewRequest request) => new Review
        {
            Description = request.Description,
            Grade = request.Grade,
            Anonymous = request.Anonymous,
            BookingNumber = request.BookingNumber
        };

        public static ReviewInfo ToDto(this Review review) => new ReviewInfo
        {
            Description = review.Description,
            Grade = review.Grade,
            FirstName = review.FirstName,
            LastName = review.LastName,
            Created = review.Created
        };

        public static RoomInfo ToDto(this Room room) => new RoomInfo
        {
            Beds = room.Beds,
            DoubleBeds = room.DoubleBeds,
            IsCondo = room.IsCondo,
            IsSuite = room.IsSuite,
            Smoking = room.Smoking,
            Pets = room.Pets,
            ImageUrl = room.ImageUrl
        };

        public static BookingInfo ToDto(this Booking booking) => new BookingInfo
        {
            Id = booking.Id,
            BookingNumber = booking.BookingNumber,
            CheckInDate = booking.CheckInDate,
            CheckOutDate = booking.CheckOutDate,
            Email = booking.Email,
            Guests = booking.Guests,
            Breakfast = booking.Breakfast,
            SpaAccess = booking.SpaAccess
        };

        public static Booking ToDomain(this BookingRequest booking) => new Booking
        {
            BookingNumber = booking.BookingNumber,
            CheckInDate = booking.CheckInDate,
            CheckOutDate = booking.CheckOutDate,
            FirstName = booking.FirstName,
            LastName = booking.LastName,
            Email = booking.Email,
            PhoneNumber = booking.PhoneNumber,
            Address = booking.Address,
            Guests = booking.Guests,
            Breakfast = booking.Breakfast,
            SpaAccess = booking.SpaAccess,
        };
    }
}
