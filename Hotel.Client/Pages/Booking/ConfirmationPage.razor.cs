using Hotel.Client.Shared.Models.Info;
using Hotel.Client.Shared.Models.Request;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Client.Pages.Booking
{
    public partial class ConfirmationPage
    {
        [Parameter] public BookingInfo ConfirmedBooking { get; set; } = new BookingInfo();
        [Parameter] public string MyName { get; set; }
    }
}
