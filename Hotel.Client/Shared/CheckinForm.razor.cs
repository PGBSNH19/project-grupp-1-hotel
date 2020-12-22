using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hotel.Client.Shared
{
    public partial class CheckinForm
    {
        [Parameter] public RoomAvailabilityRequest AvailableRoom { get; set; } = new RoomAvailabilityRequest();
        [Parameter] public EventCallback EventCallback { get; set; }
        [Inject] AppState AppState { get; set; }
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] NavigationManager Nav { get; set; }

        private List<int> numberOfGuest = new List<int> { 1, 2, 3, 4 };
        private RoomInfo[] Rooms { get; set; } // todo: pass this data to next component to show rooms
        async Task GetRoom()
        {
            if (AvailableRoom.CheckInDate > AvailableRoom.CheckOutDate || AvailableRoom.CheckInDate < DateTime.Now)
            {
                // todo: toast notification
            }
            else
            {
                AppState.SetAvailabilityRequest(AvailableRoom);

                Rooms = await Http.GetFromJsonAsync<RoomInfo[]>
                     ($"{Configuration["BaseApiUrl"]}api/v1.0/booking/check/guests/{AvailableRoom.Guests}/checkin/{AvailableRoom.CheckInDate.ToString("yy-MM-dd")}/checkout/{AvailableRoom.CheckOutDate.ToString("yy-MM-dd")}");

                if (Rooms != null)
                {
                    AppState.SetRooms(Rooms);
                    Nav.NavigateTo("booking");
                }
                else
                {
                    // todo: toast notification
                }

            }

        }
    }
}
