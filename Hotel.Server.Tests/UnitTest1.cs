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
            // fråga Mirko
            var controller = new WeatherForecastController(null);

            var result = controller.GetFoo();

            Assert.Null(result); // intentional failure
            Assert.IsType<OkResult>(result);
        }
    }
}
