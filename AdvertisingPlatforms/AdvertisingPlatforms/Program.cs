using AdvertisingPlatforms.Configurations;

namespace AdvertisingPlatforms
{
    public class Program
    {
        public static void Main(string[] args)
        {


            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll",
            //        builder =>
            //        {
            //            builder.AllowAnyOrigin() // Разрешает запросы из любого источника
            //                   .AllowAnyMethod() // Разрешает любые HTTP-методы (GET, POST и т.д.)
            //                   .AllowAnyHeader(); // Разрешает любые заголовки
            //        });
            //});


            builder.AddServices();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI();
                app.UseSwagger();
            }

            // Доступ к публичной части сайта
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            //app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
