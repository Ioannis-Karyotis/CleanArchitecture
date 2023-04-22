using WebApi.Utils.Helpers;

namespace WebApi.Utils.Extensions.Startup
{
    public static class ConfigurationExtensions
    {
        public static string GetAppActiveConnectionString(this IConfiguration config)
        {
            return
                config.GetConnectionString(EnvVariablesRetriever.GetAppActiveSchema())
                ?? "Host=localhost;Database=CleanArch;Username=postgres;Password=Margoleta16!";
        }

        public static string GetActiveDBSchema(this IConfiguration config) 
        {
            return
                config.GetConnectionString(EnvVariablesRetriever.GetAppActiveSchema())
                ?? "PgSQLData";
        } 
    }
}
