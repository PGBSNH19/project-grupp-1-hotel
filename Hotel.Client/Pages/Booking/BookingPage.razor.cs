using Hotel.Server.Models.Info;
using Hotel.Server.Models.Request;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Hotel.Client.Pages.Booking
{
    public partial class BookingPage
    {
        BookingRequest BookingRequest = new BookingRequest();
        [Inject] HttpClient Http { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }

        public async Task CreateBooking()
        {
            //var result = await Http.PostAsJsonAsync("", BookingRequest);
            var request = BookingRequest;
            Console.WriteLine(request);

            if(request != null)
            {
                navigationManager.NavigateTo($"booking/confirmation/{request.FirstName}/{request.Email}");
            }
            else
            {
                Console.WriteLine("wrong");
            }
                        
        }       
    }
}
