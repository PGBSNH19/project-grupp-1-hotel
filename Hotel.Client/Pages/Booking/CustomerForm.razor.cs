using Hotel.Server.Models.Request;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Client.Pages.Booking
{
    public partial class CustomerForm
    {
        [Parameter] public BookingRequest BookingRequest { get; set; } = new BookingRequest();
        [Parameter] public EventCallback OnValidSubmit { get; set; }

    }
}
