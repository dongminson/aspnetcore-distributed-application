using Microsoft.AspNetCore.Identity;

namespace geolocation.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}