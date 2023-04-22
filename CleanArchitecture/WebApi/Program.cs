using Serilog;
using WebApi.Utils.Extensions.Startup;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Configure Host
        builder.Host
            .ConfigureEnviromentVariables()
            .UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)); ;

        //Configure Services
        builder.Services
            .RegisterApplicationServices(builder.Configuration);

        //Prepare Web Application
        var app = builder
            .ConfigureWebHostEnviromentDefaults()
            .PrepareWebApplicationAndSeed();

        //Configure Application
        app.Configure();
    }
}