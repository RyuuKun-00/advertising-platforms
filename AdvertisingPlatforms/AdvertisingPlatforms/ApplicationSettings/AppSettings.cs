using AdvertisingPlatforms.Core.Abstractions;

namespace AdvertisingPlatforms.AplicationSettings
{
    /// <summary>
    /// Пользовательские настройки приложения
    /// </summary>
    public class AppSettings : IAppSettings
    {

        public string[] AllowedExtensions { get; }

        public string[] AllowedMimeTypes { get; }

        public int MaxSizeFile { get; }

        public bool AllowingTheUseOfCapitalLetters { get; }

        public bool CapitaLetterSensitivity { get; }

        public bool LocationsWithTheSameName { get; }

        public string StringValidationPattern { get; }

        public string DataTemplateDescription { get; }

        public string LocationTemplateDescription { get; }

        /// <summary>
        /// Инициализация пользовательских настроек приложения
        /// </summary>
        public AppSettings(IConfiguration configuration)
        {
            #region Получаем настройки валидации файла

            var fileValidationParameters = configuration.GetSection("ApplicationSettings:FileValidationParameters");


            // Получаем список допустимых расширений файла
            AllowedExtensions = fileValidationParameters.GetSection("AllowedExtensions").Get<string[]>()
                                ?? [".txt"];// По умолчанию


            // Инициализируем список разрешённых MIME типов файлов (разрешённый контекст файла)
            AllowedMimeTypes = fileValidationParameters.GetSection("AllowedMimeTypes").Get<string[]>()
                               ?? ["text/plain"];// По умолчанию


            // Инициализируем ограничение по весу файла
            MaxSizeFile = fileValidationParameters.GetValue<int>("MaxSize");
            if (MaxSizeFile == 0)
            {
                MaxSizeFile = 50 * 1024 * 1024;// 50 Мб - по умолчанию
            }

            #endregion



            #region Получаем настройки валидации рекламных платформ

            var validationParameters = configuration.GetSection("ApplicationSettings:AdvertisingPlatformValidationParameters");

            // Получение параметра на чувствительнось к символам верхнего регистра.
            CapitaLetterSensitivity = validationParameters.GetValue<bool>("CapitaLetterSensitivity");


            // Получение разрешения на использование верхнего регистра
            if (CapitaLetterSensitivity)
            {
                // Если локации чувствительны к верхнему регистру, то верхний регистр автоматически разрешён
                AllowingTheUseOfCapitalLetters = true;
            }
            else
            {
                // Получение параметра на разрешение использования верхнего регистра
                AllowingTheUseOfCapitalLetters = validationParameters.GetValue<bool>("AllowingTheUseOfCapitalLetters");
            }

            // Получение параметра на наличие локаций с одинаковыми названиями
            LocationsWithTheSameName = validationParameters.GetValue<bool>("LocationsWithTheSameName");

            // Инициализация патерна для валидации разрешённых символов
            StringValidationPattern = AllowingTheUseOfCapitalLetters
                       ? @"^[a-zA-Z/]+$" // Тут верхний регист разрешён
                       : @"^[a-z/]+$";   // Тут верхний регист вызывает ошибки

            #endregion

            #region Генерируем описание шаблона данных

            LocationTemplateDescription =
                $"""
                Требования к локациям:
                 - Локации должны начинаться и отделяться символом '/';
                 - Локации задаются символами нижнего {(AllowingTheUseOfCapitalLetters ? "и вержнего " : "")}регистра латинского алфавита.
                """;

            DataTemplateDescription = 
                $"""
                Рекламные площадки должны быть представлены в виде шаблона:
                    <Название площдадки> : <Локации работы площадки>
                Локации работы площадки перечисляются через запятую и задаются следующим шаблоном:
                    /<Локация>/<Локация>.../<Локация>
                {LocationTemplateDescription}
                """;


            #endregion
        }
    }
}
