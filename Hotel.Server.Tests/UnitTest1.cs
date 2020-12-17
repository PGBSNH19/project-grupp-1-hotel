using System;
using Xunit;
using Hotel.Server.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Server.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            // todo: kolla upp autofixture 
            // tar bort arrange-delen

            var controller = new BookingController();

            var result = await controller.GetBookingByBookingNumber("123456");

            Assert.IsType<OkResult>(result);
        }
    }
}
