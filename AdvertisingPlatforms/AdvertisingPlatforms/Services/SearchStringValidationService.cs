using AdvertisingPlatforms.Core.Abstractions;

namespace AdvertisingPlatforms.Services
{
    public class SearchStringValidationService : ISearchStringValidationService
    {
        private readonly IAppSettings _appSettings;
        private readonly IAdvertisingPlatformValidationService _validation;

        public SearchStringValidationService(IAppSettings appSettings,
                                             IAdvertisingPlatformValidationService advertisingPlatformValidation)
        {
            _appSettings = appSettings;
            _validation = advertisingPlatformValidation;
        }

        public bool IsValidSearchString(string location, out string? validSearchString)
        {
            validSearchString = null;

            string locTrim = location.Trim();

            string[]? subLocation = _validation.IsLocation(locTrim, out bool isValid);

            if (!isValid)
            {
                return false;
            }

            if (_appSettings.LocationsWithTheSameName)
            {
                validSearchString = String.Join(String.Empty, subLocation!);
            }
            else
            {
                validSearchString = subLocation![subLocation.Length - 1];
            }
            return true;
        }
    }
}
