using geolocation.Entities;
using geolocation.DTOs;
using geolocation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace geolocation.Controllers
{
    public class GeolocationController : BaseApiController
    {

        private readonly IGeolocationRepository _geolocationRepository;

        private readonly ITokenService _tokenService;
        public GeolocationController(IGeolocationRepository geolocationRepository, ITokenService tokenService)
        {
            _geolocationRepository = geolocationRepository;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAllGeolocations()
        {
            var locations = await _geolocationRepository.GetAllGeolocationsAsync();
            return Ok(locations);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetGeolocationById(int userId)
        {
            var geolocation = await _geolocationRepository.GetGeolocationByUserIdAsync(userId);
            if (geolocation == null)
            {
                return NotFound();
            }
            return Ok(geolocation);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Geolocation>> CreateGeolocation(GeolocationDto geolocationDto)
        {
            var userIdFromToken = _tokenService.GetUserIdFromToken(HttpContext.Request);

            var geolocation = new Geolocation
            {
                Latitude = geolocationDto.Latitude,
                Longitude = geolocationDto.Longitude,
                Altitude = geolocationDto.Altitude,
                UserId = userIdFromToken,
                Visibility = true,
                Created = DateTime.Now,
            };

            await _geolocationRepository.AddGeolocationAsync(geolocation);

            return geolocation;
            
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Geolocation>> UpdateGeolocation(UpdateGeolocationDto geolocationDto)
        {

            var userIdFromToken = _tokenService.GetUserIdFromToken(HttpContext.Request);
            var currentGeolocation = await _geolocationRepository.GetGeolocationByIdAsync(geolocationDto.Id);
            if (currentGeolocation == null)
            {
                return NotFound("Geolocation not found");
            }
            if (currentGeolocation.UserId != userIdFromToken)
            {
                return Unauthorized("Geolocation doesn't belong to user");
            }

            var geolocation = new Geolocation
            {
                Id = geolocationDto.Id,
                Latitude = geolocationDto.Latitude,
                Longitude = geolocationDto.Longitude,
                Altitude = geolocationDto.Altitude,
                UserId = userIdFromToken,
                Visibility = geolocationDto.Visibility,
                Created = DateTime.Now,
            };

            await _geolocationRepository.UpdateGeolocationAsync(currentGeolocation, geolocation);

            return geolocation;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Geolocation>> DeleteGeolocation(int id)
        {
            var currentGeolocation = await _geolocationRepository.GetGeolocationByIdAsync(id);
            if (currentGeolocation == null)
            {
                return NotFound("Geolocation not found");
            }

            await _geolocationRepository.DeleteGeolocationAsync(currentGeolocation);
            return currentGeolocation;
        }
    }
}