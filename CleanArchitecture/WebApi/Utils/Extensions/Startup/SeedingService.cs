using Domain.Entities;
using Infastructure;
using Microsoft.EntityFrameworkCore;


namespace WebApi.Utils.Extensions.Startup
{
    public static class SeedingService
    {
        public static WebApplication PrepareHostAndSeed(this WebApplicationBuilder builder)
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
                    new Member(
                        new Guid("c381d663-2240-4efd-8a29-84765f16a88d"),
                        "ioannis.karyotis16@gmail.com",
                        "Ioannis",
                        "Karyotis"
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
