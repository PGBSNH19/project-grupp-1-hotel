using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Server.Persistence
{
    public class HotelContext : DbContext
    {
        // todo: enable 
        //public virtual DbSet<Room> Rooms { get; set; }  
        //public virtual DbSet<Booking> Bookings { get; set; }  

        public HotelContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // todo: enable 
            //var room = builder.Entity<Room>();
            //room.HasData(new
            //{
            //    Beds = 1,
            //    DoubleBeds = 1,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = false,
            //    Pets = false
            //}, new
            //{
            //    Beds = 1,
            //    DoubleBeds = 1,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = true,
            //    Pets = false
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 0,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = true,
            //    Pets = false
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 0,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = true,
            //    Pets = true
            //}, new
            //{
            //    Beds = 1,
            //    DoubleBeds = 0,
            //    IsCondo = false,
            //    IsSuite = false,
            //    Smoking = true,
            //    Pets = false
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 1,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = true,
            //    Pets = false
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 1,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = true,
            //    Pets = false
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 0,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = false,
            //    Pets = false
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 0,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = false,
            //    Pets = false
            //}, new
            //{
            //    Beds = 1,
            //    DoubleBeds = 1,
            //    IsCondo = false,
            //    IsSuite = false,
            //    Smoking = false,
            //    Pets = true
            //}, new
            //{
            //    Beds = 1,
            //    DoubleBeds = 1,
            //    IsCondo = true,
            //    IsSuite = true,
            //    Smoking = false,
            //    Pets = true
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 2,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = false,
            //    Pets = true
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 2,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = true,
            //    Pets = false
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 1,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = true,
            //    Pets = false
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 1,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = false,
            //    Pets = true
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 0,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = false,
            //    Pets = true
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 0,
            //    IsCondo = true,
            //    IsSuite = false,
            //    Smoking = false,
            //    Pets = false
            //}, new
            //{
            //    Beds = 2,
            //    DoubleBeds = 0,
            //    IsCondo = false,
            //    IsSuite = false,
            //    Smoking = true,
            //    Pets = false
            //});

            // no Booking seed
            //var booking = builder.Entity<Booking>();
        }
    }
}
