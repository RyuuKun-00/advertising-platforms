namespace AdvertisingPlatforms.Contracts
{
    public record class ApplicationParametersResponse(
        bool AllowingTheUseOfCapitalLetters,
        bool CapitaLetterSensitivity,
        bool LocationsWithTheSameName,
        string[] AllowedExtensions,
        string[] AllowedMimeTypes,
        int MaxSize
        );
}
