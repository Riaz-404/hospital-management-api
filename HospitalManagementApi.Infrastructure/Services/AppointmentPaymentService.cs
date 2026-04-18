using System;
using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Repositories;

namespace HospitalManagementApi.Infrastructure.Services;

public class AppointmentPaymentService : IAppointmentPaymentService
{
    private readonly IRepository<AppointmentPayment> _appointmentPaymentRepository;
    private readonly IRepository<Appointment> _appointmentRepository;
    private readonly IMapper _mapper;

    public AppointmentPaymentService(
        IRepository<AppointmentPayment> appointmentPaymentRepository,
        IRepository<Appointment> appointmentRepository,
        IMapper mapper)
    {
        _appointmentPaymentRepository = appointmentPaymentRepository ?? throw new ArgumentNullException(nameof(appointmentPaymentRepository));
        _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<AppointmentPaymentDto> CreateAppointmentPaymentAsync(CreateAppointmentPaymentDto createPaymentDto)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(createPaymentDto.AppointmentId);
        if (appointment == null)
        {
            throw new ArgumentException($"Appointment with ID {createPaymentDto.AppointmentId} does not exist.");
        }

        var payment = _mapper.Map<AppointmentPayment>(createPaymentDto);
        await _appointmentPaymentRepository.AddAsync(payment);
        return _mapper.Map<AppointmentPaymentDto>(payment);
    }

    public async Task DeleteAppointmentPaymentAsync(int id)
    {
        var payment = await _appointmentPaymentRepository.GetByIdAsync(id);
        if (payment != null)
        {
            await _appointmentPaymentRepository.DeleteAsync(payment);
        }
    }

    public async Task<IEnumerable<AppointmentPaymentDto>> GetAllAppointmentPaymentsAsync()
    {
        var payments = await _appointmentPaymentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AppointmentPaymentDto>>(payments);
    }

    public async Task<AppointmentPaymentDto?> GetAppointmentPaymentByIdAsync(int id)
    {
        var payment = await _appointmentPaymentRepository.GetByIdAsync(id);
        return payment == null ? null : _mapper.Map<AppointmentPaymentDto>(payment);
    }

    public async Task<IEnumerable<AppointmentPaymentDto>> GetAppointmentPaymentsByAppointmentAsync(int appointmentId)
    {
        var payments = await _appointmentPaymentRepository.FindAsync(payment => payment.AppointmentId == appointmentId);
        return _mapper.Map<IEnumerable<AppointmentPaymentDto>>(payments);
    }

    public async Task<AppointmentPaymentDto> UpdateAppointmentPaymentAsync(UpdateAppointmentPaymentDto updatePaymentDto)
    {
        var payment = _mapper.Map<AppointmentPayment>(updatePaymentDto);
        await _appointmentPaymentRepository.UpdateAsync(payment);
        return _mapper.Map<AppointmentPaymentDto>(payment);
    }
}