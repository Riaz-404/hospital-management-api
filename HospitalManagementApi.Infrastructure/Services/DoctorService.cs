using System;
using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Repositories;

namespace HospitalManagementApi.Infrastructure.Services;

public class DoctorService : IDoctorService
{
    private readonly IRepository<Doctor> _doctorRepository;
    private readonly IMapper _mapper;

    public DoctorService(IRepository<Doctor> doctorRepository, IMapper mapper)
    {
        _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<DoctorDto> CreateDoctorAsync(CreateDoctorDto createDoctorDto)
    {
        var doctor = _mapper.Map<Doctor>(createDoctorDto);
        await _doctorRepository.AddAsync(doctor);
        return _mapper.Map<DoctorDto>(doctor);
    }

    public async Task DeleteDoctorAsync(int id)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id);
        if (doctor != null)
        {
            await _doctorRepository.DeleteAsync(doctor);
        }
    }

    public async Task<IEnumerable<DoctorDto>> GetAllDoctorsAsync()
    {
        var doctors = await _doctorRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DoctorDto>>(doctors);
    }

    public async Task<DoctorDto?> GetDoctorByIdAsync(int id)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id);
        return doctor == null ? null : _mapper.Map<DoctorDto>(doctor);
    }

    public async Task<DoctorDto> UpdateDoctorAsync(UpdateDoctorDto updateDoctorDto)
    {
        var doctor = _mapper.Map<Doctor>(updateDoctorDto);
        await _doctorRepository.UpdateAsync(doctor);
        return _mapper.Map<DoctorDto>(doctor);
    }
}
