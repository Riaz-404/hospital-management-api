using HospitalManagementApi.Application.DTOs;
namespace HospitalManagementApi.Application.Service;

public interface IAppointmentService
{
    Task<AppointmentDto?> GetAppointmentByIdAsync(int id);
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
    Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto);
    Task<AppointmentDto> UpdateAppointmentAsync(UpdateAppointmentDto updateAppointmentDto);
    Task DeleteAppointmentAsync(int id);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorAsync(int doctorId);
}
