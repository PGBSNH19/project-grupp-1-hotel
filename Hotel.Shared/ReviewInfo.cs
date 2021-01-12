using System;

namespace Hotel.Shared
{
    public class ReviewInfo
    {
        public string Description { get; set; }
        public int Grade { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
    }
}
