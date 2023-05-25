using WebApi.Utils.Helpers;

namespace WebApi.Utils.Extensions.Startup
{
    public static class ConfigurationExtensions
    {
        public static string GetAppActiveConnectionString(this IConfiguration config)
        {
            Console.WriteLine($"/////  {EnvVariablesRetriever.GetAppActiveConnectionVariable()} /////");
            var connectionString = config.GetConnectionString(EnvVariablesRetriever.GetAppActiveConnectionVariable())
                ?? "Host=localhost;Database=CleanArch;Username=postgres;Password=Margoleta16!";

            return connectionString;
        }

        public static string GetActiveDBSchema(this IConfiguration config) 
        {
            var activeDbSchema = config.GetConnectionString($"Schema{EnvVariablesRetriever.GetAppActiveSchema()}")
                ?? "CleanArchData";

            return activeDbSchema;
        } 
    }
}
