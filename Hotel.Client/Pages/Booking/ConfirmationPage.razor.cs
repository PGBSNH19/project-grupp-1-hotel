using Hotel.Shared;
using Microsoft.AspNetCore.Components;

namespace Hotel.Client.Pages.Booking
{
    public partial class ConfirmationPage
    {
        [Parameter] public BookingInfo ConfirmedBooking { get; set; } = new BookingInfo();
        [Parameter] public string MyName { get; set; }
    }
}
