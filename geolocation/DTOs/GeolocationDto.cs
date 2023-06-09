using System.ComponentModel.DataAnnotations;

namespace geolocation.DTOs
{
    public class GeolocationDto
    {
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Altitude { get; set; }
        [Required]
        public bool Visibility { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime Created { get; set; } 
    }
}