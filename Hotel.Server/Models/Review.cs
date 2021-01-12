using System;

namespace Hotel.Server.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Grade { get; set; }
        public bool Anonymous { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BookingNumber { get; set; }
        public DateTime Created { get; private set; } = DateTime.Now;
    }
}
