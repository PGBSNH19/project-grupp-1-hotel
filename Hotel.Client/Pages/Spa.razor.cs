using System.Collections.Generic;

namespace Hotel.Client.Pages
{
    public partial class Spa
    {
        List<Information> information = new List<Information>
    {
        new Information
        {
            Title = "Peace and Quiet",
            Image = "/Images/Spa/poolgirl.jpg",
            Description = "Calm, unhurried movements and hushed voices help to maintain the tranquil atmosphere at Ocean Tranquility."
        },
        new Information
        {
            Title = "Simply be",
            Image = "/Images/Spa/sauna.jpg",
            Description = "Slip down into the warm water in our hot springs, explore several different kinds of saunas and enjoy a lovely all-over massage in our Vitality pool."
        },
         new Information
        {
            Title = "Treat yourself",
            Image = "/Images/Spa/massage.jpg",
            Description = "Splurge yourself in one of our treatments."
        }
    };

        public class Information
        {
            public string Title { get; set; }
            public string Image { get; set; }
            public string Description { get; set; }

        }
    }
}
