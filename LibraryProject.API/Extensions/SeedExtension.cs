using LibraryProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.API.Extensions
{
    public static class SeedExtension
    {
        public static IHost SeedData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<LibraryContext>();

            context.Database.Migrate();

            new LibraryContextSeed(context).SeedData();

            return host;
        }
    }
}