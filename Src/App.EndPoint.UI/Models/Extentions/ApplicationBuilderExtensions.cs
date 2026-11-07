using App.Infrastructure.DataBase.SQL.SeedDate;

namespace App.EndPoint.UI.Models.Extentions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredService<IdentitySeeder>();

            await seeder.SeedAsync();
        }
    }
}
