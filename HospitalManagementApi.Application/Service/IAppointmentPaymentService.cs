using System;
using HospitalManagementApi.Application.DTOs;

namespace HospitalManagementApi.Application.Service;

public interface IAppointmentPaymentService
{
    Task<AppointmentPaymentDto?> GetAppointmentPaymentByIdAsync(int id);
    Task<IEnumerable<AppointmentPaymentDto>> GetAllAppointmentPaymentsAsync();
    Task<AppointmentPaymentDto> CreateAppointmentPaymentAsync(CreateAppointmentPaymentDto createPaymentDto);
    Task<AppointmentPaymentDto> UpdateAppointmentPaymentAsync(UpdateAppointmentPaymentDto updatePaymentDto);
    Task DeleteAppointmentPaymentAsync(int id);
    Task<IEnumerable<AppointmentPaymentDto>> GetAppointmentPaymentsByAppointmentAsync(int appointmentId);
}
