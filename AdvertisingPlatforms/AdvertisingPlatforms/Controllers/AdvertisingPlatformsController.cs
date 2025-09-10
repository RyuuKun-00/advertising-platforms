using AdvertisingPlatforms.Contracts;
using AdvertisingPlatforms.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatforms.Controllers
{
    [ApiController]
    [Route("/api/AdvertisingPlatforms")]
    public class AdvertisingPlatformsController : Controller
    {
        private readonly IAdvertisingPlatformsService _advertisingPlatformsService;
        private readonly ISearchStringValidationService _validation;
        private readonly IAppSettings _appSettings;

        public AdvertisingPlatformsController(IAdvertisingPlatformsService advertisingPlatformsService,
                                              ISearchStringValidationService searchStringValidationService,
                                              IAppSettings appSettings)
        {
            _advertisingPlatformsService = advertisingPlatformsService;
            _validation = searchStringValidationService;
            _appSettings = appSettings;
        }

        [HttpGet]
        public async Task<ActionResult<AdvertisingPlatformsResponse>> Search([FromQuery] string? search)
        {
            if (String.IsNullOrWhiteSpace(search))
            {
                return BadRequest("Строка для поиска не обнаружена.");
            }

            if (!_validation.IsValidSearchString(search, out string? validSearchString))
            {
                string error = $"Строка для поиска задана не верно. Следуйте следующим правилам.\n{_appSettings.LocationTemplateDescription}";
                return BadRequest(error);
            }
            var res = await _advertisingPlatformsService.Search(validSearchString!);
            
            return Ok(res);
        }

        [Route("SearchParameters")]
        [HttpGet]
        public ActionResult<ApplicationParametersResponse> SearchParameters()
        {
            var response = new ApplicationParametersResponse(_appSettings.AllowingTheUseOfCapitalLetters,
                                                             _appSettings.CapitaLetterSensitivity,
                                                             _appSettings.LocationsWithTheSameName,
                                                             _appSettings.AllowedExtensions,
                                                             _appSettings.AllowedMimeTypes,
                                                             _appSettings.MaxSizeFile);
             
            return Ok(response);
        }
    }
}
