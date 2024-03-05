using geolocation.Entities;
using Microsoft.EntityFrameworkCore;

namespace geolocation.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions options) : base(options) {

        }

        public DbSet<Geolocation> Geolocations { get; set; }
    }
}