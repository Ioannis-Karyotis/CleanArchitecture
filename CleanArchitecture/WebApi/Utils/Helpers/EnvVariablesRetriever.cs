namespace WebApi.Utils.Helpers
{
    public static class EnvVariablesRetriever
    {
        public static string GetAppContentRootPath() =>  Environment.GetEnvironmentVariable("APP_CONTENT_ROOT");
        public static string GetAppWebRootPath() => Environment.GetEnvironmentVariable("APP_WEB_ROOT");
        public static string GetAppActiveConnectionVariable() => Environment.GetEnvironmentVariable("ACTIVE_CONNECTION_VARIABLE");
        public static string GetAppActiveSchema() => Environment.GetEnvironmentVariable("ACTIVE_SCHEMA_VARIABLE");
    }
}
