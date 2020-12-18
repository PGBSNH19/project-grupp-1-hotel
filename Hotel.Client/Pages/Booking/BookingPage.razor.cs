﻿using Hotel.Client.Shared;
using Hotel.Client.Shared.Models.Info;
using Hotel.Client.Shared.Models.Request;
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
        BookingRequest BookingRequest = new BookingRequest();
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Configuration { get; set; }
        
        BookingInfo ConfirmedBooking { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Console.WriteLine(AppState.AvailabilityRequest.Guests);
            Console.WriteLine(AppState.AvailabilityRequest.CheckInDate);
            Console.WriteLine(AppState.AvailabilityRequest.CheckOutDate);
        }
        public async Task CreateBooking()
        {
            try { 
                BookingRequest.BookingNumber = Guid.NewGuid().ToString();
                var result = await Http.PostAsJsonAsync($"{Configuration["BaseApiUrl"]}api/v1.0/booking/", BookingRequest);
                if (result.IsSuccessStatusCode)
                {
                    ConfirmedBooking = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{BookingRequest.BookingNumber}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);                
            }
        }       
    }
}
