using Hotel.Client.Shared;
using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Hotel.Client.Pages.Booking
{
    public partial class BookingPage
    {
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] AppState AppState { get; set; }
        public BookingInfo ConfirmedBooking { get; set; }
        [Parameter] public RoomAvailabilityRequest AvailableRoom { get; set; } = new RoomAvailabilityRequest();

        [Inject] NavigationManager NavigationManager { get; set; }

        private RoomInfo[] Rooms { get; set; } // todo: pass this data to next component to show rooms

        protected override void OnInitialized()
        {
            if(AppState.AvailabilityRequest == null)
            {
                AppState.BookingRequest = new BookingRequest() { 
                    Guests = 1, 
                    CheckInDate = DateTime.Now,
                    CheckOutDate = DateTime.Now.AddDays(1)
                };
            }
            else
            {
                AppState.BookingRequest.Guests = AppState.AvailabilityRequest.Guests;
                AppState.BookingRequest.CheckInDate = AppState.AvailabilityRequest.CheckInDate;
                AppState.BookingRequest.CheckOutDate = AppState.AvailabilityRequest.CheckOutDate;
                StateHasChanged();

            }
        }

        public async Task CreateBooking()
        {
            try
            {
                AppState.BookingRequest.BookingNumber = Guid.NewGuid().ToString();
                var result = await Http.PostAsJsonAsync($"{Configuration["BaseApiUrl"]}api/v1.0/booking/", AppState.BookingRequest);
                if (result.IsSuccessStatusCode)
                {
                    ConfirmedBooking = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{AppState.BookingRequest.BookingNumber}");
                    StateHasChanged();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
                     ($"{Configuration["BaseApiUrl"]}api/v1.0/booking/check/guests/{AvailableRoom.Guests}/checkin/{AvailableRoom.CheckInDate.ToString("yy-MM-dd")}/checkout/{AvailableRoom.CheckOutDate.ToString("yy-MM-dd")}");


                if (Rooms != null)
                {

                    AppState.SetRooms(Rooms);
                    StateHasChanged();
                }
                else
                {
                    // todo: toast notification
                }

            }

        }

    }
}
