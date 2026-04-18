using HospitalManagementApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementApi.Infrastructure.Services;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DatabaseConnection");
        services.AddDbContext<HospitalDbContext>(options => 
            options.UseNpgsql(connectionString, b => b.MigrationsAssembly("HospitalManagementApi.Infrastructure"))
        );

        return services;
    }
    
}
