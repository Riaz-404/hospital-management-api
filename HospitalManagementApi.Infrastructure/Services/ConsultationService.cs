using System;
using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Data;
using HospitalManagementApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementApi.Infrastructure.Services;

public class ConsultantService : IConsultationService
{
    private readonly HospitalDbContext _hospitalDbContext;
    private readonly IRepository<Consultation> _consultationRepository;
    private readonly IRepository<Patient> _patientRepository;
    private readonly IRepository<Doctor> _doctorRepository;
    private readonly IRepository<Appointment> _appointmentRepository;
    private readonly IMapper _mapper;

    public ConsultantService(
        HospitalDbContext hospitalDbContext,
        IRepository<Consultation> consultationRepository,
        IRepository<Patient> patientRepository,
        IRepository<Doctor> doctorRepository,
        IRepository<Appointment> appointmentRepository,
        IMapper mapper)
    {
        _hospitalDbContext = hospitalDbContext ?? throw new ArgumentNullException(nameof(hospitalDbContext));
        _consultationRepository = consultationRepository ?? throw new ArgumentNullException(nameof(consultationRepository));
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }


    public async Task<ConsultationDto> CreateConsultationAsync(CreateConsultationDto createConsultationDto)
    {
        var patient = await _patientRepository.GetByIdAsync(createConsultationDto.PatientId);

        if (patient == null)
        {
            throw new ArgumentException($"Patient with ID {createConsultationDto.PatientId} does not exist.");
        }

        var doctor = await _doctorRepository.GetByIdAsync(createConsultationDto.DoctorId);

        if (doctor == null)
        {
            throw new ArgumentException($"Doctor with ID {createConsultationDto.DoctorId} does not exist.");
        }

        var appointment = await _appointmentRepository.GetByIdAsync(createConsultationDto.AppointmentId);

        if (appointment == null)
        {
            throw new ArgumentException($"Appointment with ID {createConsultationDto.AppointmentId} does not exist.");
        }

        var consultation = _mapper.Map<Consultation>(createConsultationDto);
        await _consultationRepository.AddAsync(consultation);
        return _mapper.Map<ConsultationDto>(consultation);
    }

    public async Task DeleteConsultationAsync(int id)
    {
        var consultation = await _consultationRepository.GetByIdAsync(id);
        if (consultation == null)
        {
            throw new ArgumentException($"Consultation with ID {id} does not exist.");
        }

        await _consultationRepository.DeleteAsync(consultation);
    }

    public async Task<IEnumerable<ConsultationDto>> GetAllConsultationsAsync()
    {
        var consultations = await _consultationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ConsultationDto>>(consultations);
    }

    public async Task<ConsultationWithDetailsDto?> GetConsultationByIdAsync(int id)
    {
        var consultation = await _hospitalDbContext.Consultations
            .Include(c => c.Patient)
            .Include(c => c.Doctor)
            .Include(c => c.Appointment)
            .Include(c => c.ConsultationPayment)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        return consultation == null ? null : _mapper.Map<ConsultationWithDetailsDto>(consultation);
    }

    public async Task<IEnumerable<ConsultationDto>> GetConsultationsByDoctorAsync(int doctorId)
    {
        var consultations = await _consultationRepository.FindAsync(c => c.DoctorId == doctorId);
        return _mapper.Map<IEnumerable<ConsultationDto>>(consultations);
    }

    public async Task<IEnumerable<ConsultationDto>> GetConsultationsByPatientAsync(int patientId)
    {
        var consultations = await _consultationRepository.FindAsync(c => c.PatientId == patientId);
        return _mapper.Map<IEnumerable<ConsultationDto>>(consultations);
    }

    public async Task<ConsultationDto> UpdateConsultationAsync(UpdateConsultationDto updateConsultationDto)
    {
        var consultation = await _consultationRepository.GetByIdAsync(updateConsultationDto.Id);
        if (consultation == null)
        {
            throw new ArgumentException($"Consultation with ID {updateConsultationDto.Id} does not exist.");
        }

        _mapper.Map(updateConsultationDto, consultation);
        await _consultationRepository.UpdateAsync(consultation);
        return _mapper.Map<ConsultationDto>(consultation);
    }
}
