using Hotel.Server.Models.Info;
using Hotel.Shared.Models.Request;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hotel.Client.Shared
{
    public partial class CheckinForm
    {
        [Parameter] public RoomAvailabilityRequest AvailableRoom { get; set; } = new RoomAvailabilityRequest();
        [Parameter] public EventCallback OnValidSubmit { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] HttpClient Http { get; set; }

        private List<int> numberOfGuest = new List<int> {1,2,3,4,5,6,7,8,9,10 };
        private RoomInfo[] Room { get; set; } // todo: pass this data to next component to show rooms
        async Task GetRoom()
        {
           Room = await Http.GetFromJsonAsync<RoomInfo[]>
                ($"{Configuration["BaseApiUrl"]}api/v1.0/booking/check/guests/{AvailableRoom.Guests}/checkin/{AvailableRoom.CheckInDate}/checkout/{AvailableRoom.CheckOutDate}");
        }
    }
}
