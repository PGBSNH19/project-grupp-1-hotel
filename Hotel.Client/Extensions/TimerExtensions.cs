using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Hotel.Client.Extensions
{
    public static class TimerExtensions
    {
        public static void Reset(this Timer timer)
        {
            timer.Stop();
            timer.Start();
        }
    }
}
