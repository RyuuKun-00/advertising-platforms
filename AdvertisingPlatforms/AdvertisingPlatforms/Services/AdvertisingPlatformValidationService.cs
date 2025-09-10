
using AdvertisingPlatforms.Core.Abstractions;
using AdvertisingPlatforms.Core.Models;
using System.Text.RegularExpressions;

namespace AdvertisingPlatforms.Services
{
    /// <summary>
    /// Класс валидации рекламной площадки
    /// </summary>
    public class AdvertisingPlatformValidationService : IAdvertisingPlatformValidationService
    {
        private readonly IAppSettings _appSettings;
        public AdvertisingPlatformValidationService(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public AdvertisingPlatformDTO? Validation(string line, out bool isValid)
        {
            isValid = false;

            // Проверка на паттерн
            // <название> : <локации>
            (string? namePlatform, string[]? locations) = IsNameAndLocations(line, out bool isValid_NaL);
            if (!isValid_NaL)
            {
                return null;
            }

            // Проверка на наличие запрещённых символов, повторений и паттерна:
            // /<локация>/<локация>/.../<локация>
            List<string[]>? listSubLocations = IsLocations(locations!, out bool isValid_L);
            if (!isValid_L)
            {
                return null;

            }

            isValid = true;
            AdvertisingPlatformDTO platform = new AdvertisingPlatformDTO(namePlatform!, listSubLocations!);

            return platform;
        }

        /// <summary>
        /// Проверка на соответствие входящей строки паттерну:
        /// <para>[название] : [локации]</para>
        /// </summary>
        /// <param name="line">Входящая строка</param>
        /// <param name="isValid">Результат проверки валидации</param>
        /// <returns>(название , локации) если IsValid=true</returns>
        private (string?, string[]?) IsNameAndLocations(string line, out bool isValid)
        {
            isValid = false;
            string[] data = line.Split(':');

            if (data.Length != 2)
            {
                return (null, null);
            }

            isValid = true;
            return (data[0].Trim(), data[1].Split(",", StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// Проверка на наличие запрещённых символов, повторений и паттерна:
        /// <para>/[локация]/[локация].../[локация]</para>
        /// </summary>
        /// <param name="locations">Строки локаций</param>
        /// <param name="isValid">Результат проверки валидации. true - если прошла</param>
        /// <returns>(список массивов локаций, список локацйи не прошедшие валидацию)</returns>
        private List<string[]>? IsLocations(string[] locations, out bool isValid)
        {
            isValid = false;
            List<string[]> listSubLocations = new();

            foreach (string location in locations)
            {
                string[]? sunLocations = IsLocation(location, out bool isValidLocation);

                if (isValidLocation)
                {
                    listSubLocations.Add(sunLocations!);
                }
                else
                {
                    return null;
                }
            }

            isValid = true;
            return listSubLocations;
        }

        /// <summary>
        /// Проверка строки локации, на повторение подлокаций и наличие запрещённых символов
        /// </summary>
        /// <param name="location">Строка локиции</param>
        /// <param name="isValid">Результат проверки валидации. true - если прошла</param>
        /// <returns>Массив локаций</returns>
        public string[]? IsLocation(string location, out bool isValid)
        {
            isValid = false;
            string locTrim = location.Trim();

            if(locTrim.Length == 0)
            {
                return null;
            }

            // Проверка на разрешённые символы по патерну
            if (!IsValidPatternCheck(locTrim))
            {
                return null;

            }

            // Преобразование локации к нижнему регистру, если они не чувствительны к регистру
            UppercaseResolution(locTrim);

            string[]? result = IsValidWhiteSpaceOrRepeatSubLocations(locTrim, out isValid);

            if (!isValid)
            {
                return null;
            }


            isValid = true;
            return result;
        }

        /// <summary>
        /// Проверка лакации на наличие посторонних символов
        /// </summary>
        private bool IsValidPatternCheck(string location)
        {
            // Проверка на разрешённые символы по патерну
            if (Regex.IsMatch(location, _appSettings.StringValidationPattern))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Если параметр CapitaLetterSensitivity = true,<br/>
        /// то следующие локации разные: /ru /Ru /rU /RU
        /// </summary>
        private void UppercaseResolution(string location)
        {
            if (_appSettings.CapitaLetterSensitivity) return;

            location = location.ToLowerInvariant();
        }

        /// <summary>
        /// Проверяем вложенные локации на пустые и повторяющиеся
        /// </summary>
        /// <returns>Массив подлокаций</returns>
        private string[]? IsValidWhiteSpaceOrRepeatSubLocations(string location, out bool isValid)
        {
            isValid = false;

            if (location[0] != '/')
            {
                return null;
            }

            string[] subLocations = location.Substring(1).Split('/');
            List<string> listSubLocations = new();

            foreach (string subloc in subLocations)

            {   // Проверка на пустую вложенную локацию
                if (String.IsNullOrWhiteSpace(subloc))
                {
                    return null;
                }

                // Провенрка на разрешение повторений
                if (!_appSettings.LocationsWithTheSameName)
                {   // Поиск повторения
                    if (listSubLocations.IndexOf(subloc) > -1) return null;
                }

                listSubLocations.Add(subloc);
                continue;
            }

            isValid = true;
            return listSubLocations.ToArray();
        }
    }
}
