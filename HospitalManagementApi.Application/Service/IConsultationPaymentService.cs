using System;
using HospitalManagementApi.Application.DTOs;

namespace HospitalManagementApi.Application.Service;

public interface IConsultationPaymentService
{
    Task<ConsultationPaymentDto?> GetConsultationPaymentByIdAsync(int id);
    Task<IEnumerable<ConsultationPaymentDto>> GetAllConsultationPaymentsAsync();
    Task<ConsultationPaymentDto> CreateConsultationPaymentAsync(CreateConsultationPaymentDto createPaymentDto);
    Task<ConsultationPaymentDto> UpdateConsultationPaymentAsync(UpdateConsultationPaymentDto updatePaymentDto);
    Task DeleteConsultationPaymentAsync(int id);
    Task<IEnumerable<ConsultationPaymentDto>> GetConsultationPaymentsByConsultationAsync(int consultationId);
}
