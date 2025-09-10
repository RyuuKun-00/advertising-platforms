using AdvertisingPlatforms.Core.Models;
using System.Collections;

namespace AdvertisingPlatforms.Contracts
{
    public record class AdvertisingPlatformsResponse
    (
        List<APLocation> AdvertisingPlatforms
    );
}
