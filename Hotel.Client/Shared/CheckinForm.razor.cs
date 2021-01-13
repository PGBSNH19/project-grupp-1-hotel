using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Hotel.Client.Shared
{
    public partial class CheckinForm
    {
        [Parameter] public RoomAvailabilityRequest AvailableRoom { get; set; }
        [Parameter] public EventCallback EventCallback { get; set; }
        private List<int> numberOfGuest = new List<int> { 1, 2, 3, 4 };
    }
}
