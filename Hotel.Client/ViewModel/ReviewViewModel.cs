using Hotel.Shared;
using System;

namespace Hotel.Client.ViewModel
{
    public class ReviewViewModel
    {
        public ReviewInfo Review { get; set; }
        public double Stars => Math.Ceiling((double)Review.Grade);
        public double GrayStars => Math.Ceiling(5 - (double)Review.Grade);
    }
}
