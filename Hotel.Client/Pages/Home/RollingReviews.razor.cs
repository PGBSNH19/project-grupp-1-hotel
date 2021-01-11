using Hotel.Client.ViewModel;
using Hotel.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Timers;

namespace Hotel.Client.Pages.Home
{
    public partial class RollingReviews
    {
        [Inject] HttpClient Http { get; set; }
        [Inject] IConfiguration Config { get; set; }
        public List<string> Images = new List<string> { "room2.jpg", "room3.jpg", "room4.jpg" };
        protected int displayIndex = 0;
        protected Timer timer;
        protected List<ReviewViewModel> reviews;

        protected async override Task OnInitializedAsync()
        {
            reviews = new List<ReviewViewModel>();
            var revs = await Http.GetFromJsonAsync<List<ReviewInfo>>($"{Config["BaseApiUrl"]}api/v1.0/review/random");
            foreach (var r in revs)
                reviews.Add(new ReviewViewModel { Review = r });
            StateHasChanged();
            StartTimer(3000);
        }

        private void StartTimer(int interval)
        {
            timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += ShowRandomImage;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void SlideRight()
        {
            if (displayIndex == Images.Count - 1)
            {
                displayIndex = 0;
            }
            else
            {
                displayIndex++;
            }
            
        }

        private void SlideLeft()
        {
            if (displayIndex == 0)
            {
                displayIndex = Images.Count;
            }
            displayIndex--;
        }

        private void ShowRandomImage(object sender, ElapsedEventArgs e)
        {
            var random = new Random();
            int num = random.Next(0, Images.Count);
            displayIndex = num;
            StateHasChanged();
        }
    }
}
