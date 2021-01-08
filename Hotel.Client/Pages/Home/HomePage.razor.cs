using Hotel.Client.Shared;
using Hotel.Client.Toast;
using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hotel.Client.Pages.Home
{
    public partial class HomePage
    {
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Config { get; set; }
        [Inject] AppState AppState { get; set; }
        [Inject] ToastService Toast { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public BookingInfo ConfirmedBooking { get; set; }
        public RoomAvailabilityRequest AvailableRoom { get; set; } = new RoomAvailabilityRequest();

        private RoomInfo[] Rooms { get; set; } // todo: pass this data to next component to show rooms
        protected double AverageGrade = 0;
        protected override async Task OnInitializedAsync()
        {
            AppState.Flush();
            AverageGrade = await Http.GetFromJsonAsync<double>($"{Config["BaseApiUrl"]}api/v1.0/review/average");
        }

        private async Task GetRoom()
        {
            if (AvailableRoom.CheckInDate.Date < DateTime.Now.Date)
            {
                Toast.ShowToast("CheckInDate can't happen earlier than today.", ToastLevel.Error);
                AppState.Flush(); // reset booking data on bad search
            }
            else if (AvailableRoom.CheckOutDate.Date <= DateTime.Now.Date)
            {
                Toast.ShowToast("CheckOutDate can't happen today or earlier.", ToastLevel.Error);
                AppState.Flush(); // reset booking data on bad search
            }
            else if (AvailableRoom.CheckInDate.Date >= AvailableRoom.CheckOutDate.Date)
            {
                Toast.ShowToast("CheckInDate can't happen same day or after CheckInDate.", ToastLevel.Error);
                AppState.Flush(); // reset booking data on bad search
            }
            else
            {
                Rooms = await Http.GetFromJsonAsync<RoomInfo[]>
                     ($"{Config["BaseApiUrl"]}api/v1.0/booking/check/guests/{AvailableRoom.Guests}/checkin/{AvailableRoom.CheckInDate.ToString("yyyy-MM-dd")}/checkout/{AvailableRoom.CheckOutDate.ToString("yyyy-MM-dd")}");

                if (Rooms.Length > 0)
                {
                    AppState.SetAvailabilityRequest(AvailableRoom);
                    AppState.SetRooms(Rooms);
                    NavigationManager.NavigateTo("booking");
                }
                else Toast.ShowToast("No Available Room", ToastLevel.Error);
            }

        }
    }
}
