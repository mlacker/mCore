using System.Threading.Tasks;

namespace mCore.Services.IdentityServer.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
