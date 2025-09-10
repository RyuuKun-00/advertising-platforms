using AdvertisingPlatforms.Core.Models;

namespace AdvertisingPlatforms.Core.Abstractions
{
    public interface IAdvertisingPlatformsRepository
    {
        Task<List<APLocation>> Search(string location);
    }
}