using LibraryProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LibraryProject.API.Extensions
{
    public static class DatabaseExtension
    {
        private static readonly int Timeout = 120;

        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(LibraryContext)), sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(Timeout);
                    sqlServerOptions.MigrationsAssembly("LibraryProject.API");
                });

                options.ConfigureWarnings(warnings =>
                {
                    warnings.Log(RelationalEventId.TransactionError);
                    warnings.Log(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning);
                });
            });
        }
    }
}