using LibraryProject.API.Settings;

namespace LibraryProject.API.Extensions
{
    public static class PoliciesExtension
    {
        public static void ConfigurePolicies(this IServiceCollection services, PoliciesConfig policies)
        {
            policies.AllowPolicies.ForEach(policy =>
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(name: policy.Name, builder =>
                    {
                        foreach (var url in policy.Allowed)
                        {
                            builder.WithOrigins(url)
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                        }
                    });
                });
            });
        }

        public static void UsePolicies(this IApplicationBuilder app, PoliciesConfig config)
        {
            config.AllowPolicies.ForEach(policy => app.UseCors(policy.Name));
        }
    }
}