using geolocation.Interfaces;
using geolocation.Services;
using geolocation.Data;
using Microsoft.EntityFrameworkCore;

namespace geolocation.Extensions
{
    public static class ApplicationServiceExtenstions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IGeolocationRepository, GeolocationRepository>();
            services.AddDbContext<DataContext>(options => {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}