using Hotel.Client.Toast;
using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Hotel.Client.Shared
{
    public partial class CheckinForm
    {
        [Parameter] public RoomAvailabilityRequest AvailableRoom { get; set; }
        [Parameter] public EventCallback EventCallback { get; set; }
        [Inject] AppState AppState { get; set; }
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] NavigationManager Nav { get; set; }

        private List<int> numberOfGuest = new List<int> { 1, 2, 3, 4 };
        [Inject] ToastService Toast { get; set; }
        private RoomInfo[] Rooms { get; set; } // todo: pass this data to next component to show rooms

        private void UpdateCheckIn(string s)
        {
            if (DateTime.TryParse(s, out DateTime t))
                AvailableRoom.CheckInDate = t;
        }

        private void UpdateCheckOut(string s)
        {
            if (DateTime.TryParse(s, out DateTime t))
                AvailableRoom.CheckOutDate = t;
        }
    }
}
