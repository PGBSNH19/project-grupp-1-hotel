//using Hotel.Shared;
//using Microsoft.AspNetCore.Components;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;

//namespace Hotel.Client.Pages.Booking
//{
//    public partial class Cancelation
//    {
//        [Inject] HttpClient Http { get; set; }
//        [Inject] IConfiguration Configuration { get; set; }

//        [Parameter]
//        public int bookingNumber { get; set; }
//        public BookingInfo bookinginfo { get; set; }
//        protected override async Task OnInitializedAsync()
//        {
//            bookinginfo = await Http.GetFromJsonAsync<BookingInfo>($"{Configuration["BaseApiUrl"]}api/v1.0/booking/{bookingNumber}");
//        }

//    }
//}
