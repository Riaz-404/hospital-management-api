using System;
using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Data;
using HospitalManagementApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementApi.Infrastructure.Services;

public class DoctorService : IDoctorService
{
    private readonly HospitalDbContext _dbContext;
    private readonly IRepository<Doctor> _doctorRepository;
    private readonly IMapper _mapper;

    public DoctorService(HospitalDbContext dbContext, IRepository<Doctor> doctorRepository, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
        var doctor = await _dbContext.Doctors
            .Include(d => d.Appointments)
            .Include(d => d.Consultations)
            .FirstOrDefaultAsync(d => d.Id == id);
        
        return doctor == null ? null : _mapper.Map<DoctorWithDetailsDto>(doctor);
    }

    public async Task<DoctorDto> UpdateDoctorAsync(UpdateDoctorDto updateDoctorDto)
    {
        var doctor = _mapper.Map<Doctor>(updateDoctorDto);
        await _doctorRepository.UpdateAsync(doctor);
        return _mapper.Map<DoctorDto>(doctor);
    }
}
