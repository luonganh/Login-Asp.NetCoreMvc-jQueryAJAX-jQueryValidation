using Asp.NetCore.Infrastructure.Identity.DataSeed;

namespace Asp.NetCore.Web.Admin.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            // Add db context
            services.AddDbContext<IdentityContext>(options =>
            {
                var connectionString = configuration.GetConnectionString(Utilities.AppSettings.ConnectionString);
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
            });

            // Add more another db contexts
            // services.AddDbContext<EXAMPLE_Context>();

            services.AddScoped(typeof(DbContext), typeof(IdentityContext));
            services.AddScoped<IDataSeeder, DataSeeder>();
            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Identity service
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;

                // Sets the minimum length a password must be
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //options.Lockout.MaxFailedAccessAttempts = 3;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            // services.AddTransient<IIdentityService, IdentityService>();

            // autoregister DI
            var assemblyToScan = Assembly.GetAssembly(typeof(IBaseService));
            services.RegisterAssemblyPublicNonGenericClasses(assemblyToScan)
                .Where(c => c.Name.EndsWith("Service") || c.Name.EndsWith("Services"))
                .AsPublicImplementedInterfaces();

            // System.Text.JSON, formatting is camelCase
            services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                });
            //// Add Newtonsoft.Json-based JSON format support and the default formatting is camelCase
            //services.AddControllers().AddNewtonsoftJson().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //});

            // Cross-Origin Resource Sharing
            // All
            services.AddCors(options => options.AddPolicy(Utilities.AppSettings.CorsPolicy, builder =>
            {
                builder.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
            }));
            //// Custom 
            //services.AddCors(options =>
            //{
            //    var corsPolicy = configuration[$"{AppSettings.CorsPolicy}:{AppSettings.AccessUrls}"].Split(',');
            //    options.AddPolicy(AppSettings.CorsPolicy, builder => builder.WithOrigins(corsPolicy)
            //        .SetIsOriginAllowedToAllowWildcardSubdomains()
            //        .WithMethods("GET", "POST", "DELETE", "OPTIONS")
            //        .AllowCredentials()
            //        .AllowAnyHeader());
            //});

            return services;           
        }
    }
}
