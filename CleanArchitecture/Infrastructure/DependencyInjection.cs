using Microsoft.Extensions.DependencyInjection;
using Infastructure;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Domain.Repositories;
using Infrastructure.Services;
using Infrastructure.Intefaces;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IDatabaseSeeding, DatabaseSeeding>();

        return services;
    }
}
