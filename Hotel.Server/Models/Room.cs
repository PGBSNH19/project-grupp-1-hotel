using System;
namespace Hotel.Server.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Beds { get; set; }
        public int DoubleBeds { get; set; }
        public bool IsCondo { get; set; } = false;
        public bool IsSuite { get; set; } = false;
        public bool Smoking { get; set; } = false;
        public bool Pets { get; set; } = false;
        public string ImageUrl { get; set; } = "";
    }
}
