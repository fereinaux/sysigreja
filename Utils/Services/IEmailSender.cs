using System.Threading.Tasks;

namespace Utils.Services
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string message);
    }
}
