using System;
using HospitalManagementApi.Application.DTOs;

namespace HospitalManagementApi.Application.Service;

public interface IConsultationService
{
    Task<ConsultationDto?> GetConsultationByIdAsync(int id);
    Task<IEnumerable<ConsultationDto>> GetAllConsultationsAsync();
    Task<ConsultationDto> CreateConsultationAsync(CreateConsultationDto createConsultationDto);
    Task<ConsultationDto> UpdateConsultationAsync(UpdateConsultationDto updateConsultationDto);
    Task DeleteConsultationAsync(int id);
    Task<IEnumerable<ConsultationDto>> GetConsultationsByPatientAsync(int patientId);
    Task<IEnumerable<ConsultationDto>> GetConsultationsByDoctorAsync(int doctorId);

}
