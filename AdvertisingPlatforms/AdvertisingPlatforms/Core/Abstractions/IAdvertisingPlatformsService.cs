using AdvertisingPlatforms.Contracts;

namespace AdvertisingPlatforms.Core.Abstractions
{
    public interface IAdvertisingPlatformsService
    {
        Task<AdvertisingPlatformsResponse> Search(string location);
    }
}