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
            //            builder.AllowAnyOrigin() // ��������� ������� �� ������ ���������
            //                   .AllowAnyMethod() // ��������� ����� HTTP-������ (GET, POST � �.�.)
            //                   .AllowAnyHeader(); // ��������� ����� ���������
            //        });
            //});


            builder.AddServices();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI();
                app.UseSwagger();
            }

            // ������ � ��������� ����� �����
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            //app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
