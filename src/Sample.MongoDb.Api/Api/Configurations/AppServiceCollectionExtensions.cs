using Sample.MongoDb.Api.Application;

namespace Sample.MongoDb.Api.Api.Configurations;

public static class AppServiceCollectionExtensions
{
    public static void ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(ApplicationEntryPoint)));

        //services.AddAppConections(configuration);
        services.ConfigureInterfacesDependencie(); 
    }
}
