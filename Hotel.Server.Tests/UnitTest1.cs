using System;
using Xunit;
using Hotel.Server.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Server.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // todo: kolla upp autofixture 
            // tar bort arrange-delen
            var controller = new WeatherForecastController(null, null);

            var result = controller.GetFoo();

            Assert.IsType<OkResult>(result);
        }
    }
}
