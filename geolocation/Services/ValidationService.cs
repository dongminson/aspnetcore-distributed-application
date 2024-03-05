using geolocation.Entities;
using geolocation.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace geolocation.Services
{
    public class ValidationService : IValidationService
    {
        public List<ValidationResult> ValidateGeolocation(Geolocation geolocation)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (geolocation.Latitude < -90 || geolocation.Latitude > 90)
            {
                validationResults.Add(new ValidationResult("Latitude is out of range"));
            }

            if (geolocation.Longitude < -180 || geolocation.Longitude > 180)
            {
                validationResults.Add(new ValidationResult("Longitude is out of range"));
            }

            if (geolocation.Altitude < 0)
            {
                validationResults.Add(new ValidationResult("Altitude cannot be negative"));
            }

            return validationResults;
        }
    }
}