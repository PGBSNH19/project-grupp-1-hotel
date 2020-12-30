using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Hotel.Client.Pages.Booking
{
    public partial class ConfirmationPage
    {
        [Parameter] public BookingInfo ConfirmedBooking { get; set; } = new BookingInfo();
        [Parameter] public string MyName { get; set; }

        public string CheckOutDate()
        {
            return ConfirmedBooking.CheckOutDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

        }
        public string CheckInDate()
        {
            return ConfirmedBooking.CheckInDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);

        }
    }
}
