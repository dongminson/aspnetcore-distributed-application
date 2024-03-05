using Microsoft.AspNetCore.Identity;

namespace user.Entities
{
    public class AppUser : IdentityUser<int>
    {

        public byte[] PasswordSalt { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}