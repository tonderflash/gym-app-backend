using Microsoft.Extensions.DependencyInjection;
using MyGymApp.Infrastructure.Context;

namespace MyGymApp.Infrastructure;

public static class IoC
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IGeneralDbContext, GeneralDbContext>();
        
        return services;
    }
}