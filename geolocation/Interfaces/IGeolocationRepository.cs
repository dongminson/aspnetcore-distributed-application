using geolocation.Entities;

namespace geolocation.Interfaces
{
    
    public interface IGeolocationRepository
    {
        Task<IEnumerable<Geolocation>> GetAllGeolocationsAsync();
        Task<Geolocation> GetGeolocationByUserIdAsync(int id);
        Task<Geolocation> GetGeolocationByIdAsync(int id);
        Task ChangeVisibility(int userId, bool visibility);
        Task AddGeolocationAsync(Geolocation geolocation);
        Task UpdateGeolocationAsync(Geolocation oldGeolocation, Geolocation newGeolocation);
        Task DeleteGeolocationAsync(Geolocation geolocation);
    }
}