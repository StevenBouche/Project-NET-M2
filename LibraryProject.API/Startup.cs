using FluentValidation.AspNetCore;
using LibraryProject.API.Extensions;
using LibraryProject.API.Hubs;
using LibraryProject.API.Settings;
using LibraryProject.Business.BookBusiness;
using LibraryProject.Business.GenreBusiness;
using LibraryProject.Business.Validators.BookValidators;
using LibraryProject.Business.Validators.GenreValidators;
using MicroElements.Swashbuckle.FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library API", Version = "v1" });
            });

            services.AddFluentValidationRulesToSwagger();
            services.ConfigureMapper();
            services.ConfigurePolicies(policiesConfig);
            services.ConfigureDb(Configuration);

            // Services for controller
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IGenreService, GenreService>();

            //Validators
            services.AddFluentValidation(x =>
            {
                x.DisableDataAnnotationsValidation = true;
                x.RegisterValidatorsFromAssemblyContaining<GenreFormCreateDtoValidator>();
                x.RegisterValidatorsFromAssemblyContaining<BookFormCreateDtoValidator>();
                x.RegisterValidatorsFromAssemblyContaining<BookFormUpdateDtoValidator>();
            });

            //Hub
            services.AddSignalR();
            services.AddTransient<LibraryProxyHub>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1"));
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
                endpoints.MapHub<LibraryHub>("/libraryhub");
            });

            app.UseScopedSwagger();



        }
    }
}