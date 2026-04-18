using HospitalManagementApi.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementApi.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => {
            cfg.AddMaps(typeof(MappingProfile).Assembly);
        });

        return services;
    }
}
