using AdvertisingPlatforms.AplicationSettings;
using AdvertisingPlatforms.Core.Abstractions;
using AdvertisingPlatforms.Core.Models;

namespace AdvertisingPlatforms.Services
{
    public class DataInitializationService : IDataInitializationService
    {
        private readonly IAdvertisingPlatformValidationService _validation;
        private readonly IAdvertisingPlatformsStorageBuilder _builder;
        private readonly IAppSettings _appSettings;

        public DataInitializationService(IAdvertisingPlatformValidationService advertisingPlatformValidation,
                                         IAdvertisingPlatformsStorageBuilder finiteStateMachineBuilder,
                                         IAppSettings appSettings)
        {
            _validation = advertisingPlatformValidation;
            _builder = finiteStateMachineBuilder;
            _appSettings = appSettings;
        }
        /// <summary>
        /// Метод загрузки данных из файла
        /// </summary>
        /// <returns>Ошибка если есть</returns>
        public async Task<string?> UploadDataFile(IFormFile pathFile)
        {
            using (var reader = new StreamReader(pathFile.OpenReadStream()))
            {
                string? line;

                // Считываем по строчно файл
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    // Валидация, при успехе возвращает десериализованный объект
                    AdvertisingPlatformDTO? platform = _validation.Validation(line, out bool isValid);

                    // Если есть хоть одна не валидная строка, то файл не валиден.
                    if (isValid)
                    {
                        // Добавляем платформу в хранилище
                        _builder.AddAdvertisingPlatform(platform!);
                    }
                    else
                    {
                        string error = $"В файле одна или несколько ошибок. Следуйте следующим правилам.\n{_appSettings.DataTemplateDescription}";
                        return error;
                    }

                }
            }

            _builder.Build();

            return null;
        }
    }
}
