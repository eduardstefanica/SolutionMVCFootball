using Serilog.Events;
using Serilog;
using WebAppMVCFootball.Models;

namespace WebAppMVCFootball
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<FootballContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Logging.ClearProviders();   // NO Microsoft Logging - VEDERE Sezione Logging di appsettings.json

            // SERILOG
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build())
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)  // NO Microsoft Logging
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Host.UseSerilog(logger);

            // app.UseSerilogRequestLogging();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}