using ClashOfMusic.Api.Configuration.Abstractions;
using ClashOfMusic.Api.Configuration.Seeding;

namespace ClashOfMusic.Api.Extensions
{
    public static class WebApplicationExtention
    {
        public static async Task CallDBSeed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var seedData = scope.ServiceProvider.GetRequiredService<ISeedDataToDB>();
                await seedData.SeedNeccessaryData();
            }
        }
    }
}
