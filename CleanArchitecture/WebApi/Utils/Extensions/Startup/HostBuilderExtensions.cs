using Microsoft.AspNetCore.Hosting;
using WebApi.Utils.Extensions.Configurations;
using WebApi.Utils.Helpers;

namespace WebApi.Utils.Extensions.Startup
{
    public static partial class HostBuilderExtensions
    {


        public static IHostBuilder ConfigureEnviromentVariables(this IHostBuilder hostbuilder)
        {
            hostbuilder
                .ConfigureAppConfiguration((hostContext, config) => config.AddMainConfiguration());

            return hostbuilder;
        }

    }
}
