using AdvertisingPlatforms.AplicationSettings;
using AdvertisingPlatforms.Core.Abstractions;
using AdvertisingPlatforms.DataAccess.Repositories;
using AdvertisingPlatforms.DataAccess.Storage;
using AdvertisingPlatforms.Services;

namespace AdvertisingPlatforms.Configurations
{
	public static class Services
	{
		public static void AddServices(this WebApplicationBuilder builder)
		{
			var s = builder.Services;

			s.AddControllers();
			s.AddEndpointsApiExplorer();
			s.AddSwaggerGen(c =>
			{
				c.CustomSchemaIds(r => r.FullName);
			});


			s.AddScoped<    IAdvertisingPlatformsRepository,	   AdvertisingPlatformsRepository>();
			s.AddScoped<    IAdvertisingPlatformsService,		   AdvertisingPlatformsService>();
			s.AddSingleton< IAdvertisingPlatformsStorage,		   AdvertisingPlatformsStorage>();
			s.AddScoped<    IAdvertisingPlatformsStorageBuilder,   AdvertisingPlatformsStorageBuilder>();
			s.AddScoped<    IAdvertisingPlatformValidationService, AdvertisingPlatformValidationService>();
			s.AddSingleton< IAppSettings,                          AppSettings>();
			s.AddScoped<    IDataInitializationService,			   DataInitializationService>();
			s.AddScoped<    IFileValidationService,			       FileValidationService>();
			s.AddScoped<    ISearchStringValidationService, 	   SearchStringValidationService>();

		}
	}
}
