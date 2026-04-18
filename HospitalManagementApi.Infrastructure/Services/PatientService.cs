using System;
using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Repositories;

namespace HospitalManagementApi.Infrastructure.Services;

public class PatientService : IPatientService
{
    private readonly IRepository<Patient> _patientRepository;
    private readonly IMapper _mapper;

    public PatientService(IRepository<Patient> patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto)
    {
        var patient = _mapper.Map<Patient>(createPatientDto);
        await _patientRepository.AddAsync(patient);
        return _mapper.Map<PatientDto>(patient);
    }

    public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
    {
        var patients = await _patientRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<PatientDto>>(patients);
    }

    public async Task<PatientDto?> GetPatientByIdAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        return patient == null ? null : _mapper.Map<PatientDto>(patient);
    }

    public async Task<PatientDto> UpdatePatientAsync(UpdatePatientDto updatePatientDto)
    {
        var patient = _mapper.Map<Patient>(updatePatientDto);
        await _patientRepository.UpdateAsync(patient);
        return _mapper.Map<PatientDto>(patient);
    }

    public async Task DeletePatientAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient != null)
        {
            await _patientRepository.DeleteAsync(patient);
        }
    }
}
