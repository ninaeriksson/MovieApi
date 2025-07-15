using Microsoft.EntityFrameworkCore;
using MovieCore.DomainContracts;
using MovieData.Context;
using MovieData.Extensions;
using MovieData.Repositories;


namespace MovieApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MovieApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MovieApiContext") ?? 
                throw new InvalidOperationException("Connection string 'MovieApiContext' not found.")));


            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                var config = app.Configuration;

                // Hämtas från appsettings.Development.json
                bool shouldSeed = config.GetValue<bool>("SeedData");

                if (shouldSeed)
                {
                    await app.SeedDataAsync();
                }
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
