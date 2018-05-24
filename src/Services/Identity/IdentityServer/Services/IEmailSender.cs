using System.Threading.Tasks;

namespace mCore.Services.IdentityServer.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
