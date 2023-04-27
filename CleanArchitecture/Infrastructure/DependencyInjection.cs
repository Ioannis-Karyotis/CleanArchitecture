using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;
using Gatherly.Infastructure;
using Application.Interfaces.Repositories;

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
