using LibraryProject.API.Extensions;
using LibraryProject.API.Settings;
using LibraryProject.Business.Common;
using LibraryProject.Business.GenreBusiness;

namespace LibraryProject.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly PoliciesConfig policiesConfig = new();

        public Startup(IWebHostEnvironment env)
        {
            var basePath = $"{env.ContentRootPath}/AppSettings";

            Configuration = BuildConfig(basePath, env.EnvironmentName, "");
            Configuration.GetSection(nameof(PoliciesConfig)).Bind(policiesConfig);
        }

        private static IConfiguration BuildConfig(string basePath, string environmentName, string name)
        {
            var path = string.IsNullOrEmpty(name) ? basePath : $"{basePath}/{name}";
            var file = string.IsNullOrEmpty(name) ? $"appsettings.{environmentName}.json" : $"appsettings.{name}.{environmentName}.json";

            return new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(file, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            // Add services to the container.
            services.AddControllers();

            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.ConfigureMapper();
            services.ConfigurePolicies(policiesConfig);
            services.ConfigureDb(Configuration);

            // Services for controller
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IGenreService, GenreService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AssignmentAPI v1"));
            }

            app.UsePolicies(policiesConfig);
            //app.UseHttpsRedirection();

            app.UseRouting();

            // Security
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}