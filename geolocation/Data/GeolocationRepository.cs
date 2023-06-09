using geolocation.Entities;
using geolocation.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace geolocation.Data
{
    public class GeolocationRepository : IGeolocationRepository
    {
        private readonly DataContext _context;
        public GeolocationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Geolocation>> GetAllGeolocationsAsync()
        {
            return await _context.Geolocations.ToListAsync();
        }

        public async Task<Geolocation> GetGeolocationByUserIdAsync(int UserId)
        {
            return await _context.Geolocations
                .Where(l => l.UserId == UserId)
                .OrderByDescending(l => l.Created)
                .FirstOrDefaultAsync();

        }

        public async Task<Geolocation> GetGeolocationByIdAsync(int Id)
        {
            return await _context.Geolocations
                .Where(l => l.Id == Id)
                .OrderByDescending(l => l.Created)
                .FirstOrDefaultAsync();

        }

        public async Task AddGeolocationAsync(Geolocation geolocation)
        {
            _context.Geolocations.Add(geolocation);
            await _context.SaveChangesAsync();
        }
        public async Task ChangeVisibility(int userId, bool visibility)
        {
            var geolocations = _context.Geolocations.Where(p => p.UserId == userId).ToList();
            foreach (var geolocation in geolocations)
            {
                geolocation.Visibility = visibility;
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGeolocationAsync(Geolocation oldGeolocation, Geolocation newGeolocation)
        {
            if (oldGeolocation != null)
            {
                _context.Entry(oldGeolocation).CurrentValues.SetValues(newGeolocation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGeolocationAsync(Geolocation geolocation)
        {
            if (geolocation != null)
            {
                _context.Geolocations.Remove(geolocation);
                await _context.SaveChangesAsync();
            }
        }
    }
}