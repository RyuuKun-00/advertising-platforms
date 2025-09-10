namespace AdvertisingPlatforms.Core.Models
{
    /// <summary>
    /// Модель для передачи данных от валидации к билдеру для создания конечного автомата
    /// </summary>
    public record class AdvertisingPlatformDTO(string name, List<string[]> locations);
}
