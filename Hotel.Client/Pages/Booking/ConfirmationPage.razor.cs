using Hotel.Client.ViewModel;
using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Hotel.Client.Pages.Booking
{
    public partial class ConfirmationPage
    {
        [Parameter] public BookingInfo ConfirmedBooking { get; set; } = new BookingInfo();
        [Parameter] public string MyName { get; set; }
        protected RoomViewModel room;

        protected override void OnInitialized() => room = new RoomViewModel { RoomInfo = ConfirmedBooking.Room };
        public string CheckOutDate() => ConfirmedBooking.CheckOutDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
        public string CheckInDate() => ConfirmedBooking.CheckInDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
    }
}
