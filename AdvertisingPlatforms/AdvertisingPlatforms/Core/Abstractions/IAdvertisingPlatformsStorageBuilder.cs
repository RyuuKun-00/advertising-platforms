using AdvertisingPlatforms.Core.Models;

namespace AdvertisingPlatforms.Core.Abstractions
{
    public interface IAdvertisingPlatformsStorageBuilder
    {
        void AddAdvertisingPlatform(AdvertisingPlatformDTO platform);
        IAdvertisingPlatformsStorage Build();
    }
}