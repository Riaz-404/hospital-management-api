using HospitalManagementApi.Application.DTOs;

namespace HospitalManagementApi.Application.Service;

public interface IDoctorService
{
    Task<DoctorDto?> GetDoctorByIdAsync(int id);
    Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync();
    Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto createDoctorDto);
    Task<DoctorDto> UpdateDoctorAsync(UpdateDoctorDto updateDoctorDto);
    Task DeleteDoctorAsync(int id);
}
