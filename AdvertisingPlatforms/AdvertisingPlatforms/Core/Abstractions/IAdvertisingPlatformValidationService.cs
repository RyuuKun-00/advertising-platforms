using AdvertisingPlatforms.Core.Models;

namespace AdvertisingPlatforms.Core.Abstractions
{
    public interface IAdvertisingPlatformValidationService
    {
        string[]? IsLocation(string location, out bool isValid);
        AdvertisingPlatformDTO? Validation(string line, out bool isValid);
    }
}