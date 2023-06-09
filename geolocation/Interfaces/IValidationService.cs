using System.ComponentModel.DataAnnotations;
using geolocation.Entities;

namespace geolocation.Interfaces
{
    public interface IValidationService
    {
        List<ValidationResult> ValidateGeolocation(Geolocation geolocation);

    }
}