using Hotel.Client.Shared;
using Hotel.Client.Toast;
using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hotel.Client.Pages.Booking
{
    public partial class BookingPage
    {
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        [Inject] AppState AppState { get; set; }
        public BookingInfo ConfirmedBooking { get; set; }
        RoomAvailabilityRequest AvailabilityRequest { get; set; } = new RoomAvailabilityRequest();
        private RoomInfo[] Rooms { get; set; }

        [Inject] ToastService Toast { get; set; }
        protected override void OnInitialized()
        {
            AppState.BookingRequest.Guests = AppState.AvailabilityRequest.Guests;
            AppState.BookingRequest.CheckInDate = AppState.AvailabilityRequest.CheckInDate;
            AppState.BookingRequest.CheckOutDate = AppState.AvailabilityRequest.CheckOutDate;
            StateHasChanged();
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
                else
                {
                    Toast.ShowToast("Booking Failed", ToastLevel.Error);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        async Task GetRoom()
        {
            if (AvailabilityRequest.CheckInDate >= AvailabilityRequest.CheckOutDate || AvailabilityRequest.CheckInDate < DateTime.Now || AvailabilityRequest.CheckOutDate <= DateTime.Now)
            {
                Toast.ShowToast("Invalid Date", ToastLevel.Error);
                AppState.Flush(); // reset booking data on bad search
            }
            else
            {
                Rooms = await Http.GetFromJsonAsync<RoomInfo[]>
                     ($"{Configuration["BaseApiUrl"]}api/v1.0/booking/check/guests/{AvailabilityRequest.Guests}/checkin/{AvailabilityRequest.CheckInDate.ToString("yy-MM-dd")}/checkout/{AvailabilityRequest.CheckOutDate.ToString("yy-MM-dd")}");

                if (Rooms != null)
                {
                    AppState.Flush(); // reset booking data on no results
                    AppState.SetAvailabilityRequest(AvailabilityRequest);
                    AppState.SetRooms(Rooms);
                    StateHasChanged();
                }
                else
                {
                    Toast.ShowToast("No Available Room", ToastLevel.Error);
                }
            }
        }
    }
}
