using MobileProviderAPI.Model;

namespace MobileProviderAPI.Data.Svc
{
    public interface IUserService
    {
        Task<UserModel?> AuthenticateAsync(string username, string password);
    }
}