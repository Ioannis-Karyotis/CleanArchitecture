using Domain.Entities;
using Domain.ValueObjects;
using Infastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WebApi.Utils.Helpers;

namespace WebApi.Utils.Extensions.Startup
{
    public static partial class WebApplicationBuilderExtensions
    {
        public static string appContentRoot = EnvVariablesRetriever.GetAppContentRootPath();
        public static string appWebRoot = EnvVariablesRetriever.GetAppWebRootPath();

        public static WebApplicationBuilder ConfigureWebHostEnviromentDefaults(this WebApplicationBuilder builder)
        {
            if (!String.IsNullOrEmpty(appContentRoot))
            {
                builder.Environment.ContentRootPath = appContentRoot;
            }

            if (!String.IsNullOrEmpty(appWebRoot))
            {
                builder.Environment.WebRootPath = appWebRoot;
            }

            return builder;
        }

        public static WebApplication PrepareWebApplicationAndSeed(this WebApplicationBuilder builder)
        {
            var webApplication = builder.Build();

            using (var scope = webApplication.Services.CreateScope())
            {
                var servicesScope = scope.ServiceProvider.CreateScope();
                var services = servicesScope.ServiceProvider;
                try
                {
                    services.SeedingContainer();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            return webApplication;
        }

        //Awaiter Methods

        public static void SeedTestMemberAwaiter(
            this IServiceProvider services,
            ApplicationDbContext appDbContext) 
        => services.SeedTestMember(appDbContext).GetAwaiter().GetResult();


        public static void SeedingContainer(this IServiceProvider services)
        {
            using var _dbContext = services.GetRequiredService<ApplicationDbContext>();

            //Prepare Needed Migrations
            _dbContext.Database.Migrate();

            //Call Seeding Services
            services.SeedTestMemberAwaiter(_dbContext);
        }

        public static async Task SeedTestMember(this IServiceProvider services, ApplicationDbContext context)
        {
            var member = await context
                .Members
                .AsNoTracking()
                .Where(m => m.Id == new Guid("c381d663-2240-4efd-8a29-84765f16a88d"))
                .FirstOrDefaultAsync();


            if (member is null)
            {

                context.Members.Add(
                    Member.Create(
                        new Guid("c381d663-2240-4efd-8a29-84765f16a88d"),
                        Email.Create("ioannis.karyotis16@gmail.com").Value,
                        FirstName.Create("Ioannis").Value,
                        LastName.Create("Karyotis").Value
                        )
                    );

                if (await context.SaveChangesAsync() > 0)
                {
                    member = null;
                }

            }
        }
    }
}
