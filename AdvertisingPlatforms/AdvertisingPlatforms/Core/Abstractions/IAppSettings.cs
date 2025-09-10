namespace AdvertisingPlatforms.Core.Abstractions
{
    /// <summary>
    /// Пользовательские настройки приложения
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Список допустимых расширений файла<br/>
        /// По умолчанию: <b>[".txt"]</b>
        /// </summary>
        string[] AllowedExtensions { get; }
        /// <summary>
        /// Список допустимых MIME типов файла<br/>
        /// По умолчанию: <b>["text/plain"]</b>
        /// </summary>
        string[] AllowedMimeTypes { get; }
        /// <summary>
        /// Разрешение на использование символов верхнего регистра
        /// <para>
        /// Если параметр AllowingTheUseOfCapitalLetters = true и <see cref="CapitaLetterSensitivity">CapitaLetterSensitivity</see> = false,<br/>
        /// то следующие локации одинаковые: <b>/ru /Ru /rU /RU</b><br/><br/>
        /// Если параметр AllowingTheUseOfCapitalLetters = false и <see cref="CapitaLetterSensitivity">CapitaLetterSensitivity</see> = false<br/>
        /// то файл с локациями в верхнем регистром не валиден: <b>/Ru /rU /RU -> Ошибки</b>
        /// </para>
        /// По умолчанию: <b>false</b>
        /// </summary>
        bool AllowingTheUseOfCapitalLetters { get; }
        /// <summary>
        /// Параметр чувствительности к символам верхнего регистра
        /// <para>
        /// Если параметр CapitaLetterSensitivity = true, то следующие локации разные: /ru /Ru /rU /RU
        /// </para>
        /// По умолчанию: <b>false</b>
        /// </summary>
        bool CapitaLetterSensitivity { get; }
        /// <summary>
        /// Разрешение на использование локаций с одинаковыми названиями
        /// <para>
        /// Если параметр LocationsWithTheSameName = true,<br/>
        /// то следующая локация типа: /ru/ru - валидна
        /// </para>
        /// По умолчанию: <b>false</b>
        /// </summary>
        bool LocationsWithTheSameName { get; }
        /// <summary>
        /// Максимальный размер файла<br/>
        /// По умолчанию: <b>50 Мб</b>
        /// </summary>
        int MaxSizeFile { get; }
        /// <summary>
        /// Патерн для валидации разрешённых символов
        /// <para>
        /// Если <see cref="AllowingTheUseOfCapitalLetters">AllowingTheUseOfCapitalLetters</see> = true,<br/>
        /// то значение: <b>@"^[a-zA-Z/]+$"</b> - верхний регистр разрешён<br/>
        /// иначе значение:<b> @"^[a-z/]+$"</b> - верхний регистр генирирует отслеживаемые ошибки
        /// </para>
        /// </summary>
        string StringValidationPattern { get; }
        /// <summary>
        /// Описание шаблона данных в файле
        /// <para>
        /// Возвращаем этот элемент если в данных файла есть ошибка
        /// </para>
        /// </summary>
        string DataTemplateDescription { get; }
        /// <summary>
        /// Описание шаблона локации в файле
        /// <para>
        /// Возвращаем этот элемент если в локации есть ошибка
        /// </para>
        /// </summary>
        string LocationTemplateDescription { get; }
    }
}