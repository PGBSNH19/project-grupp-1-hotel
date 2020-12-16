using Hotel.Server.Models.Info;
using Hotel.Server.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Models
{
    public static class ModelExtensions
    {
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
