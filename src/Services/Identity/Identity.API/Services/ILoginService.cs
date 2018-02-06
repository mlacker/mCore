namespace mCore.Services.Identity.API.Services
{
    using System.Threading.Tasks;

    public interface ILoginService<T>
    {
        Task<bool> ValidateCredentials(T user, string password);

        Task<T> FindByUsername(string user);

        Task SignIn(T user);
    }
}
