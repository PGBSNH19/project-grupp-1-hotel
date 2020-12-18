
using Hotel.Client.Shared.Models.Request;
using Microsoft.AspNetCore.Components;

namespace Hotel.Client.Pages.Booking
{
    public partial class CustomerForm
    {
        [Parameter] public BookingRequest BookingRequest { get; set; } = new BookingRequest();
        [Parameter] public EventCallback OnValidSubmit { get; set; }

    }
}
