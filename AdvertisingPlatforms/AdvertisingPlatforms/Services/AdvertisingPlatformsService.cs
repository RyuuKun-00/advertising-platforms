using AdvertisingPlatforms.Contracts;
using AdvertisingPlatforms.Core.Abstractions;

namespace AdvertisingPlatforms.Services
{
    public class AdvertisingPlatformsService : IAdvertisingPlatformsService
    {
        private readonly IAdvertisingPlatformsRepository _repository;

        public AdvertisingPlatformsService(IAdvertisingPlatformsRepository advertisingPlatformsRepository)
        {
            _repository = advertisingPlatformsRepository;
        }

        public async Task<AdvertisingPlatformsResponse> Search(string location)
        {


            var listAdvertisingPlatforms = await _repository.Search(location);

            var result = new AdvertisingPlatformsResponse(listAdvertisingPlatforms);

            return result;
        }
    }
}
