using Hotel.Client.Extensions;
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
            StartTimer(5000);
        }

        private void StartTimer(int interval)
        {
            timer = new Timer
            {
                Interval = interval,
                AutoReset = true,
                Enabled = true
            };
            timer.Elapsed += ShowRandomImage;
        }

        private void SlideRight()
        {
            timer.Reset();
            if (displayIndex == Images.Count - 1)
                displayIndex = 0;
            else
                displayIndex++;
            StateHasChanged();
        }

        private void SlideLeft()
        {
            timer.Reset();
            if (displayIndex == 0)
                displayIndex = Images.Count;
            displayIndex--;
            StateHasChanged();
        }

        private void ShowRandomImage(object sender, ElapsedEventArgs e) => SlideRight();
    }
}
