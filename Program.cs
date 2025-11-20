using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcBookshelf.Data;
using MvcBookshelf.Models;
using MvcBookshelf.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MvcBookshelf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MvcBookshelfContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MvcBookshelfContext") ?? throw new InvalidOperationException("Connection string 'MvcBookshelfContext' not found.")));
            // add scoped with bookshelf service

            // add health check services before the builder https://www.youtube.com/watch?v=9ntrl3KNCpo
            // utilize EntityFramework core DbContext probe https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-9.0#entity-framework-core-dbcontext-probe
            // and install the correct NuGet package, with .NET version of 9.0 instead of latest 10.0 for compatability
            builder.Services.AddHealthChecks().AddDbContextCheck<MvcBookshelfContext>(); // otherwise add health check for SQL serv
            // custom health check using SampleHealthCheck isn't needed with AddDbContextCheck
            // builder.Services.AddHealthChecks().AddCheck<SampleHealthCheck>("Sample");
            builder.Services.AddScoped<IBookshelfService,BookshelfService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // read secret via Configuration API
            // https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-9.0&tabs=windows
            var mvcBookshelfSecret = builder.Configuration["MvcBookshelf:ServiceApiKey"];

            var app = builder.Build();

            // map into app api endpoint
            app.MapHealthChecks("/healthz");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                // add logging
                // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-10.0#log-in-programcs
                app.Logger.LogInformation("Seeding initial data");
                SeedData.Initialize(services);
            }


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            // app.MapGet("/", () => mvcBookshelfSecret); don't need to read secret
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Logger.LogInformation("Run app");
            app.Run();
        }
    }
}
