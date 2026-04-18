using HospitalManagementApi.Application.DTOs;

namespace HospitalManagementApi.Application.Service;

public interface IPatientService
{
    Task<PatientDto?> GetPatientByIdAsync(int id);
    Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
    Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto);
    Task<PatientDto> UpdatePatientAsync(UpdatePatientDto updatePatientDto);
    Task DeletePatientAsync(int id);
}
