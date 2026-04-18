using System;
using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Repositories;

namespace HospitalManagementApi.Infrastructure.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IRepository<Appointment> _appointmentRepository;
    private readonly IRepository<Patient> _patientRepository;
    private readonly IRepository<Doctor> _doctorRepository;
    private readonly IMapper _mapper;

    public AppointmentService(IRepository<Appointment> appointmentRepository, IRepository<Patient> patientRepository, IRepository<Doctor> doctorRepository, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
    {
        var patient = await _patientRepository.GetByIdAsync(createAppointmentDto.PatientId);
        var doctor = await _doctorRepository.GetByIdAsync(createAppointmentDto.DoctorId);

        if (patient == null)
        {
            throw new ArgumentException($"Patient with ID {createAppointmentDto.PatientId} does not exist.");
        }

        if (doctor == null)
        {
            throw new ArgumentException($"Doctor with ID {createAppointmentDto.DoctorId} does not exist.");
        }

        var appointment = _mapper.Map<Appointment>(createAppointmentDto);
        await _appointmentRepository.AddAsync(appointment);
        return _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task DeleteAppointmentAsync(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment != null)
        {
            await _appointmentRepository.DeleteAsync(appointment);
        }
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
    {
        var appointments = await _appointmentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }

    public async Task<AppointmentDto?> GetAppointmentByIdAsync(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        return appointment == null ? null : _mapper.Map<AppointmentDto>(appointment);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByDoctorAsync(int doctorId)
    {
        var appointments = await _appointmentRepository.FindAsync(a => a.DoctorId == doctorId);
        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId)
    {
        var appointments = await _appointmentRepository.FindAsync(a => a.PatientId == patientId);
        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }

    public async Task<AppointmentDto> UpdateAppointmentAsync(UpdateAppointmentDto updateAppointmentDto)
    {
        var appointment = _mapper.Map<Appointment>(updateAppointmentDto);
        await _appointmentRepository.UpdateAsync(appointment);
        return _mapper.Map<AppointmentDto>(appointment);
    }
}
