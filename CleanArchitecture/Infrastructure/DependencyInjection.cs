using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;
using Domain.Repositories;
using Gatherly.Infastructure;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        return services;
    }
}
