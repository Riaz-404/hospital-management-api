using System;
using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Repositories;

namespace HospitalManagementApi.Infrastructure.Services;

public class ReportService : IReportService
{
    private readonly IRepository<Report> _reportRepository;
    private readonly IRepository<Patient> _patientRepository;
    private readonly IRepository<Test> _testRepository;
    private readonly IMapper _mapper;

    public ReportService(
        IRepository<Report> reportRepository,
        IRepository<Patient> patientRepository,
        IRepository<Test> testRepository,
        IMapper mapper)
    {
        _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        _testRepository = testRepository ?? throw new ArgumentNullException(nameof(testRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ReportDto> CreateReportAsync(CreateReportDto createReportDto)
    {
        var patient = await _patientRepository.GetByIdAsync(createReportDto.PatientId);
        if (patient == null)
        {
            throw new ArgumentException($"Patient with ID {createReportDto.PatientId} does not exist.");
        }

        var test = await _testRepository.GetByIdAsync(createReportDto.TestId);
        if (test == null)
        {
            throw new ArgumentException($"Test with ID {createReportDto.TestId} does not exist.");
        }

        var report = _mapper.Map<Report>(createReportDto);
        await _reportRepository.AddAsync(report);
        return _mapper.Map<ReportDto>(report);
    }

    public async Task DeleteReportAsync(int id)
    {
        var report = await _reportRepository.GetByIdAsync(id);
        if (report != null)
        {
            await _reportRepository.DeleteAsync(report);
        }
    }

    public async Task<IEnumerable<ReportDto>> GetAllReportsAsync()
    {
        var reports = await _reportRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReportDto>>(reports);
    }

    public async Task<ReportDto?> GetReportByIdAsync(int id)
    {
        var report = await _reportRepository.GetByIdAsync(id);
        return report == null ? null : _mapper.Map<ReportDto>(report);
    }

    public async Task<IEnumerable<ReportDto>> GetReportsByPatientAsync(int patientId)
    {
        var reports = await _reportRepository.FindAsync(report => report.PatientId == patientId);
        return _mapper.Map<IEnumerable<ReportDto>>(reports);
    }

    public async Task<IEnumerable<ReportDto>> GetReportsByTestAsync(int testId)
    {
        var reports = await _reportRepository.FindAsync(report => report.TestId == testId);
        return _mapper.Map<IEnumerable<ReportDto>>(reports);
    }

    public async Task<ReportDto> UpdateReportAsync(UpdateReportDto updateReportDto)
    {
        var report = _mapper.Map<Report>(updateReportDto);
        await _reportRepository.UpdateAsync(report);
        return _mapper.Map<ReportDto>(report);
    }
}