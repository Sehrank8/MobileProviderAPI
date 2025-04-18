using MobileProviderAPI.Model;
namespace MobileProviderAPI.Model
{
    public static class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>
        {
            new UserModel
            {
                Username = "admin",
                Password = "password",
                EmailAddress = "admin@example.com",
                GivenName = "Admin",
                Surname = "User",
                Role = "Administrator"
            },
            new UserModel
            {
                Username = "user",
                Password = "1234",
                EmailAddress = "user@example.com",
                GivenName = "Standard",
                Surname = "User",
                Role = "User"
            }
        };
    }
}