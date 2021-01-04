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
using Hotel.Shared;

namespace Hotel.Server.Services
{
    public class EmailService : IEmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string RoomTypeInfo(Booking entity)
        {
            return (entity.Room.Beds, entity.Room.DoubleBeds) switch
            {
                (1, 0) => "Single bed",
                (1, 1) => "Double bed",
                (2, 2) => "Master suite",
                (2, 1) => "Triple bed",
                (2, 0) => "Twin bed",
                _ => string.Empty
            };
        }

        public void SendMessage(Booking entity)
        {
            var emailUsername = _configuration.GetSection("EmailService").GetSection("EmailUserName").Value;
            var emailPassword = _configuration.GetSection("EmailService").GetSection("EmailPassword").Value;
            var emailSmtpHost = _configuration.GetSection("EmailService").GetSection("EmailSmtpHost").Value;
            var emailSmtpPort = _configuration.GetSection("EmailService").GetSection("EmailSmtpPort").Value;
            var mailbody = CreateMessageContent(entity);
            var message = CreateMailMessage(entity, mailbody, emailUsername, "Hotell Group");

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

        private MailMessage CreateMailMessage(Booking entity, StringBuilder mailbody, string emailUsername, string displayName)
        {
            MailMessage message = new MailMessage();
            message.To.Add(entity.Email);
            message.From = new MailAddress(emailUsername, displayName);
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
            string roomType = RoomTypeInfo(entity);

            string breakfastIncluded = entity.Breakfast? $"Breakfast - Included<br>":"";
            string spaIncluded = entity.SpaAccess ? "Spa Access - Included<br>" : "";

            string created = entity.Created.ToString("dd/M/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            mailbody.Append($"" +
                            $"<div style='background: #eee; margin: 20px; padding: 20px;'>" +
                            $"<center><h1>Hotell </h1><br>" +
                            $"<h4> We look forward to your stay {entity.FirstName}.</h4>" +
                            $"<font> Check in is between 12:00 - 16:00.<br>" +
                            $"Checkout before 12:00 on your last day.<br>" +
                            $"You have booked a room between the dates {checkin}" +
                            $"- {checkout}.<br></font><br><br>" +
                            $"<b>Booking No<b>: {entity.BookingNumber} <br><br></center>" +
                            $"<font><b> Your booking details:</b><br><br>" +
                            $"First name - {entity.FirstName}<br>" +
                            $"Last name - {entity.LastName}<br>" +
                            $"Phone number - {entity.PhoneNumber}<br>" +
                            $"Address - {entity.Address}<br>" +
                            $"E-mail - {entity.Email}<br>" +
                            $"Number of guests - {entity.Guests}<br>" +
                            $"Room type - {roomType}<br>" +
                            $"Check In - {checkin}<br>" +
                            $"Check Out - {checkout}<br>" +
                            $"{spaIncluded}" +
                            $"{breakfastIncluded}" +
                            $"Created at - {created}</font></div>"
                            );
                            

            return mailbody;
        }


    }
}
