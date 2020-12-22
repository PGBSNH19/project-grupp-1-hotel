using Hotel.Client.Shared;
using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Timers;

namespace Hotel.Client.Pages.Home
{
    public partial class HomePage
    {
        public List<string> Images = new List<string> { "room2.jpg", "room3.jpg", "room4.jpg", "room5.jpg" };
        public int ImageIndex = 0;
        public string NextIndicator = ">>";
        public string BackIndicator = "<<";
        Timer timer;

        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Config { get; set; }

        [Inject] AppState AppState { get; set; }
        public BookingInfo ConfirmedBooking { get; set; }
        public RoomAvailabilityRequest AvailableRoom { get; set; } = new RoomAvailabilityRequest();

        [Inject] NavigationManager NavigationManager { get; set; }

        private RoomInfo[] Rooms { get; set; } // todo: pass this data to next component to show rooms

        protected override async Task OnInitializedAsync()
        {
            var result = await Http.GetFromJsonAsync<BookingInfo>($"{Config["BaseApiUrl"]}api/v1.0/booking/foo");
            Console.WriteLine(result.Email);
            StartTimer(3000);
        }

        private void SlideRight()
        {
            if (ImageIndex == Images.Count - 1)
            {
                ImageIndex = 0;
            }
            ImageIndex++;
        }

        private void SlideLeft()
        {
            if (ImageIndex == 0)
            {
                ImageIndex = Images.Count;
            }
            ImageIndex--;
        }

        private void StartTimer(int interval)
        {
            timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += ShowRandomImage;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void ShowRandomImage(object sender, ElapsedEventArgs e)
        {
            var random = new Random();
            int num = random.Next(0, Images.Count);
            ImageIndex = num;
            StateHasChanged();
        }

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
                     ($"{Config["BaseApiUrl"]}api/v1.0/booking/check/guests/{AvailableRoom.Guests}/checkin/{AvailableRoom.CheckInDate.ToString("yy-MM-dd")}/checkout/{AvailableRoom.CheckOutDate.ToString("yy-MM-dd")}");


                if (Rooms != null)
                {
                    AppState.SetRooms(Rooms);
                    NavigationManager.NavigateTo("booking");
                }
                else
                {
                    // todo: toast notification
                }

            }

        }
    }
}
