using AdvertisingPlatforms.Core.Abstractions;
using AdvertisingPlatforms.Core.Models;

namespace AdvertisingPlatforms.DataAccess.Repositories
{
    public class AdvertisingPlatformsRepository : IAdvertisingPlatformsRepository
    {
        private readonly IAdvertisingPlatformsStorage _storage;

        public AdvertisingPlatformsRepository(IAdvertisingPlatformsStorage storage)
        {
            _storage = storage;
        }

        public async Task<List<APLocation>> Search(string location)
        {
            var result = await _storage.Search(location);

            return result;
        }
    }
}
