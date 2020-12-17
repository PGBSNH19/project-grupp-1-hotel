using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override void OnInitialized()
        {
            StartTimer(3000);
        }

        private void SlideRight()
        {
            if (ImageIndex == Images.Count- 1)
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
    }
}
