using System.ComponentModel.DataAnnotations;

namespace MobileProviderAPI.Model
{
    public class UserModel
    {
        [Key]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Role { get; set; }
    }
}