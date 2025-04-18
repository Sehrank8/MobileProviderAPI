namespace MobileProviderAPI.Data.Svc
{
    using global::MobileProviderAPI.Context;
    using global::MobileProviderAPI.Model;
    using Microsoft.EntityFrameworkCore;


    namespace MobileProviderAPI.Services
    {
        public class UserService : IUserService
        {
            private readonly BillingContext _context;

            public UserService(BillingContext context)
            {
                _context = context;
            }

            public async Task<UserModel?> AuthenticateAsync(string username, string password)
            {
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            }
        }
    }
}