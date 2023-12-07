using Sample.MongoDb.Api.Domain.Interfaces;
using Sample.MongoDb.Api.Infra.Repositories;

namespace Sample.MongoDb.Api.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ConfigureInterfacesDependencie(this IServiceCollection services)
    {
        services.AddSingleton<Infra.MongoDB>();
        services.AddScoped<IRestaurantRepository,RestaurantRepository>();
        services.AddScoped<RestaurantRepository>();
        return services;
    }
}
