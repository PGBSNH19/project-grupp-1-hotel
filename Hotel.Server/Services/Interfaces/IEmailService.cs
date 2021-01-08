using Hotel.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Server.Services.Interfaces
{
    public interface IEmailService
    {
        void SendMessage(Booking entity);
    }
}
