using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Data;
using HospitalManagementApi.Infrastructure.Repositories;
using HospitalManagementApi.Application.Service;
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

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IRepository<Patient>, Repository<Patient>>();
        services.AddScoped<IRepository<Doctor>, Repository<Doctor>>();
        services.AddScoped<IRepository<Appointment>, Repository<Appointment>>();
        services.AddScoped<IRepository<Consultation>, Repository<Consultation>>();
        services.AddScoped<IRepository<Test>, Repository<Test>>();
        services.AddScoped<IRepository<Payment>, Repository<Payment>>();
        services.AddScoped<IRepository<Report>, Repository<Report>>();

        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IConsultationService, ConsultantService>();
        services.AddScoped<ITestService, TestService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IAppointmentPaymentService, AppointmentPaymentService>();
        services.AddScoped<IConsultationPaymentService, ConsultationPaymentService>();
        services.AddScoped<ITestPaymentService, TestPaymentService>();

        return services;
    }
    
}
