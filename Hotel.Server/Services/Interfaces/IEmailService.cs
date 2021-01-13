using Hotel.Server.Models;

namespace Hotel.Server.Services.Interfaces
{
    public interface IEmailService
    {
        void SendMessage(Booking entity);
    }
}
