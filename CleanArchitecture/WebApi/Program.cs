using Serilog;
using WebApi.Utils.Extensions.Startup;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Configure HostBuilder of WebApplicationBuilder
        builder.Host
            .ConfigureEnviromentVariables()
            .UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)); ;

        //Configure Services
        builder.Services
            .RegisterApplicationServices(builder.Configuration);

        //Configure WebApplicationBuilder and Prepare WebApplication
        var app = builder
            .ConfigureWebHostEnviromentDefaults()
            .PrepareWebApplicationAndSeed();

        //Configure WebApplication
        app.Configure();
    }
}