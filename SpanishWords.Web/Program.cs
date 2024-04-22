using EFDataAccess.DataAccess;
using SpanishWords.EntityFramework.Repositories;
using SpanishWords.EntityFramework.Repositories.Infrastructure;
using EFDataAccess.Repositories;
using EFDataAccess.Repositories.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SpanishWords.Models;
using System.IdentityModel.Tokens.Jwt;
using NLog;
using NLog.Web;

namespace SpanishWords.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Early init of NLog to allow startup and exception logging, before host is built
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("init main");
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                // Add services to the container.
               
                builder.Services.AddControllersWithViews();
                builder.Services.AddDbContext<WordsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
                builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WordsContext>();
                builder.Services.AddScoped<IWordRepository, WordRepository>();
                builder.Services.AddScoped<IStatsRepository, StatsRepository>();
                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

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
                app.MapRazorPages();

                app.Run();

            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }

        }
    }
}