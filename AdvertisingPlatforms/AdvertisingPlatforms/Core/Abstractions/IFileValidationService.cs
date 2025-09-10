namespace AdvertisingPlatforms.Core.Abstractions
{
    /// <summary>
    /// Сервис валидации файла
    /// </summary>
    public interface IFileValidationService
    {
        /// <summary>
        /// Валидация файла на соответствие допустимому расширению, типу контента и размеру
        /// </summary>
        /// <param name="uploadedFile">Загружаемый файл</param>
        /// <param name="errorValid">Ошибки валидации</param>
        /// <returns>Возвращает <b>true</b>, если файл прошёл валидацию</returns>
        bool IsValid(IFormFile uploadedFile, out string? error);
    }
}