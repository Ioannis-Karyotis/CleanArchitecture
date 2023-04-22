using Application.Models.Configuration;

namespace WebApi.Utils.Extensions.Configurations
{
    public static class AppConfigurationsExtensions
    {
        public static void AddMainConfiguration(this IConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .AddJsonFile($"{Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "Configs", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development",
                    "appsettings.json")}",
                optional: true,
                reloadOnChange: true
            );
        }

        public static void ConfigBindClasses(this IServiceCollection services, IConfiguration config)
        {   
            //HERE WILL BIND CONFIGURATION CLASSES
            //EXAMPLE
            services.Configure<TestConfiguration>(config.GetSection("TestConfiguration"));
        }
    }
}