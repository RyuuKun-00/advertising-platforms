using AdvertisingPlatforms.Core.Abstractions;

namespace AdvertisingPlatforms.Services
{
    /// <summary>
    /// Класс валидации файла
    /// </summary>
    public class FileValidationService : IFileValidationService
    {
        private readonly IAppSettings _appSettings;

        /// <summary>
        /// Конструктор класса валидации файла
        /// </summary>
        public FileValidationService(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public bool IsValid(IFormFile uploadedFile, out string? errorValid)
        {
            // Проверка на наличие файла
            if (!IsExists(uploadedFile, out errorValid)) return false;
            // Проверка на допустимое расширение файла
            if (!IsValid_Extension(uploadedFile,out errorValid)) return false;
            // Проверка на допустимый контент файла
            if (!IsValid_MIMEType(uploadedFile, out errorValid)) return false;
            // Проверка на допустимый размер файла
            if (!IsValid_FileSize(uploadedFile, out errorValid)) return false;

            return true;
        }

        /// <summary>
        /// Проверка на наличие файла
        /// </summary>
        private bool IsExists(IFormFile formFile, out string? error)
        {
            error = null;
            // Проверка на наличие файла
            if (formFile == null || formFile.Length == 0)
            {
                error = "Файл не обнаружен.";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка на допустимое расширение файла
        /// </summary>
        private bool IsValid_Extension(IFormFile formFile, out string? error)
        {
            error = null;

            // Получение допустимых расширений файлов
            string[] allowedExtensions = _appSettings.AllowedExtensions;
            string fileExtension = Path.GetExtension(formFile.FileName).ToLower();

            // Проверка на соответсвие допустимого расширения файла
            if (!allowedExtensions.Contains(fileExtension))
            {
                error = $"""
                        Недопустимый тип файла. 
                        Разрешены только: {String.Join(", ", allowedExtensions)}.
                        """;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка на допустимый контент файла
        /// </summary>
        private bool IsValid_MIMEType(IFormFile formFile, out string? error)
        {
            error = null;

            // Получение допутимых MIME типов
            string[] allowedMimeTypes = _appSettings.AllowedMimeTypes;
            string mimeType = formFile.ContentType.ToLower();

            if (!allowedMimeTypes.Contains(mimeType))
            {
                error = $"""
                        Недопустимый контент файла. 
                        Разрешены только: {String.Join(", ", allowedMimeTypes)}.
                        """;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверка на допустимый размер файла
        /// </summary>
        private bool IsValid_FileSize(IFormFile formFile, out string? error)
        {
            error = null;

            // Получение максимального размера файла
            int maxSize = _appSettings.MaxSizeFile;
            if (maxSize == 0)
            {
                maxSize = 50 * 1024 * 1024;// 50 Мб\
            }

            // Проверка на размер файла
            if (formFile.Length > maxSize)
            {
                error = $"""
                        Недопустимый размер файла. 
                        Максимальный разрешенный размер: {maxSize} байт.
                        """;
                return false;
            }

            return true;
        }
    }
}
