using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Models.Info
{
    public class RoomInfo
    {
        public int Id { get; set; }
        public int Beds { get; set; }
        public int DoubleBeds { get; set; }
        public bool IsCondo { get; set; }
        public bool IsSuite { get; set; }
        public bool Smoking { get; set; }
        public bool Pets { get; set; }
        public string ImageUrl { get; set; }
    }
}