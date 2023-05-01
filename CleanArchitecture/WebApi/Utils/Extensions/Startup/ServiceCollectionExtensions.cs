using Application;
using Infastructure;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Presentation;
using WebApi.Utils.Extensions.Configurations;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using System;

namespace WebApi.Utils.Extensions.Startup
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationServices(
            this IServiceCollection services,
            ConfigurationManager configurationManager)
        {

            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseNpgsql(
            //        configurationManager.GetAppActiveConnectionString(),
            //        assembly => assembly.MigrationsHistoryTable("__MyMigrationsHistory",
            //            configurationManager.GetActiveDBSchema())
            //    )
            //);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(configurationManager.GetAppActiveConnectionString(),
                    assembly => assembly.MigrationsHistoryTable("__MyMigrationsHistory",
                    configurationManager.GetActiveDBSchema())
                )
            );


            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services
                .AddApplication()
                .AddInfrastructure()
                .AddPresentation();

            services.ConfigBindClasses(configurationManager);

            return services;
        }
    }
}
