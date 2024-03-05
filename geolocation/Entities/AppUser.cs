using Microsoft.AspNetCore.Identity;

namespace geolocation.Entities
{
    public class AppUser : IdentityUser<int>
    {

        public byte[] PasswordSalt { get; set; }

        public bool Visibility { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}