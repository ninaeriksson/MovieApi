using Microsoft.EntityFrameworkCore;
using MovieApi.Data;
using System.Diagnostics;

namespace MovieApi.Extensions
{
    public static class WebApplicationExtensions
    {
        //Anropas endast om shouldSeed är true i appsettings.Development.json
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<MovieApiContext>();

                //await context.Database.EnsureDeletedAsync(); //Tar bort hela databasen helt och hållet
                //await context.Database.MigrateAsync();

                try
                {
                    // Rensa data i tabellerna utan att radera databasen
                    await SeedData.ClearDatabaseAsync(context);

                    await SeedData.InitAsync(context);
                }
                catch (Exception ex)
                {                   // sätta debuggern på denna rad när seeda data!!
                    Debug.WriteLine(ex);
                    throw;
                }
            }
        }
    }
}
