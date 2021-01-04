using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Hotel.Server.Models;
using System.Globalization;
using System.Net;
using Hotel.Server.Services.Interfaces;

namespace Hotel.Server.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMessage(Booking entity)
        {
            var emailUsername = _configuration.GetSection("EmailService").GetSection("EmailUserName").Value;
            var emailPassword = _configuration.GetSection("EmailService").GetSection("EmailPassword").Value;
            var emailSmtpHost = _configuration.GetSection("EmailService").GetSection("EmailSmtpHost").Value;
            var emailSmtpPort = _configuration.GetSection("EmailService").GetSection("EmailSmtpPort").Value;
            var mailbody = CreateMessageContent(entity);
            var message = CreateMailMessage(entity, mailbody);

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = emailUsername,
                    Password = emailPassword
                };
                smtp.Credentials = credential;
                smtp.Host = emailSmtpHost;
                smtp.Port = int.Parse(emailSmtpPort);
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
        }

        private MailMessage CreateMailMessage(Booking entity, StringBuilder mailbody)
        {
            MailMessage message = new MailMessage();
            message.To.Add(entity.Email);
            message.From = new MailAddress("hotellgruppett@gmail.com", "Hotell Group");
            message.Subject = $"Booking Details for {entity.FirstName} {entity.LastName}";
            message.Body = mailbody.ToString();
            message.IsBodyHtml = true;

            return message;
        } 
        private StringBuilder CreateMessageContent(Booking entity)
        {
            StringBuilder mailbody = new StringBuilder();

            string checkin = entity.CheckInDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            string checkout = entity.CheckOutDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            string breakfastIncluded = entity.Breakfast? $"Breakfast - Included<br>":"";
            string spaIncluded = entity.SpaAccess ? "Spa Access - Included<br>" : "";

            mailbody.Append($"" +
                            $"<div style='background: #eee; margin: 20px; padding: 20px;'>" +
                            $"<center><h1>Hotell </h1><br>" +
                            $"<h4> We look forward to your stay.</h4>" +
                            $"<font> Check in is between 12:00 - 4:00 PM.<br>" +
                            $"Checkout before 12:00 PM on your last day.<br>" +
                            $"You have booked a room between the dates {checkin}" +
                            $"- {checkout}.<br></font><br><br>" +
                            $"<b>Booking No<b>: {entity.BookingNumber} <br><br></center>" +
                            $"<font><b> Your booking details:</b><br><br>" +
                            $"Number of guests - {entity.Guests}<br>" +
                            $"Check In - {checkin}<br>" +
                            $"Check Out - {checkout}<br>" +
                            $"{spaIncluded}" +
                            $"{breakfastIncluded}</font></div>");

            return mailbody;
        }


    }
}
