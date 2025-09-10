using AdvertisingPlatforms.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingPlatforms.Controllers
{
    [ApiController]
    [Route("/api")]
    public class FileUploadController : Controller
    {
        private readonly IFileValidationService _fileValidation;
        private readonly IDataInitializationService _dataInitService;
        public FileUploadController(IFileValidationService fileValidation, IDataInitializationService dataInitializationService)
        {
            _fileValidation = fileValidation;
            _dataInitService = dataInitializationService;
        }

        

        [Route("upload")]
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile uploadedFile)
        {
            try
            {
                // Валидация файла
                if (!_fileValidation.IsValid(uploadedFile, out string? error))
                {
                    return BadRequest(error);
                }

                // Проверка данных файла
                error = await _dataInitService.UploadDataFile(uploadedFile);

                if (error is not null)
                {
                    return BadRequest(error);
                }

                return Ok("Инициализация данных прошла успешно!");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
