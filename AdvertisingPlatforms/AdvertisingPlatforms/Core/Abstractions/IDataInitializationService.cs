
namespace AdvertisingPlatforms.Core.Abstractions
{
    public interface IDataInitializationService
    {
        Task<string?> UploadDataFile(IFormFile pathFile);
    }
}