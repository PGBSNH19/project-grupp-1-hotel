using Hotel.Shared.Models.Request;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Client.Shared
{
    public partial class CheckinForm
    {
        [Parameter] public BookingRequest Booking { get; set; } = new BookingRequest();
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        private List<int> NumberOfGuest = new List<int> {1,2,3,4,5,6,7,8,9,10 };
    }
}
