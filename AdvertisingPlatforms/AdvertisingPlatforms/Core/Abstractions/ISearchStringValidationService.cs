namespace AdvertisingPlatforms.Core.Abstractions
{
    public interface ISearchStringValidationService
    {
        bool IsValidSearchString(string location, out string? validSearchString);
    }
}