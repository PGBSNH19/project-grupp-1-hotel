using Hotel.Client.Toast;
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
    public partial class Review
    {
        [Inject] HttpClient Http { get; set; }
        [Inject] ToastService Toast { get; set; }
        [Inject] IConfiguration Config { get; set; }
        public int ReviewIndex { get; private set; }
        public ReviewInfo[] Reviews { get; set; }
        public List<CssIcon> StarIcons = new List<CssIcon>();

        Timer timer;
        protected override async Task OnInitializedAsync()
        {
            Reviews = await Http.GetFromJsonAsync<ReviewInfo[]>($"{Config["BaseApiUrl"]}api/v1.0/review/average");
            if (Reviews != null)
            {
                StartTimer(5000);
            }
            else
            {
                Toast.ShowToast("No Reviews Found", ToastLevel.Error);
            }
        }

        private void StartTimer(int interval)
        {
            timer = new Timer();
            timer.Interval = interval;
            timer.Elapsed += ShowReview;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void ShowReview(object sender, ElapsedEventArgs e)
        {
            int reviewsAmount = Reviews.Length;
            var random = new Random();
            int index = random.Next(0, reviewsAmount);
            SetStars(index);
            StateHasChanged();
        }

        private void SetStars(int index)
        {
            ReviewIndex = index;
            int grade = Reviews[ReviewIndex].Grade;
            StarIcons.Clear();
            StarIcons.AddRange(GetStars());

            for (int i = 0; i <= grade; i++)
            {
                StarIcons[i].Star = "fa fa-star checked";
            }
        }

        private List<CssIcon> GetStars()
        {
            return new List<CssIcon>
            {
                new CssIcon{Star="fa fa-star"},
                new CssIcon{Star="fa fa-star"},
                new CssIcon{Star="fa fa-star"},
                new CssIcon{Star="fa fa-star"},
                new CssIcon{Star="fa fa-star"}, 
            };
        }

        public class CssIcon
        {
            public string Star { get; set; }
        }
    }

}
