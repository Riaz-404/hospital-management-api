using System;
using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Repositories;

namespace HospitalManagementApi.Infrastructure.Services;

public class TestService : ITestService
{
    private readonly IRepository<Test> _testRepository;
    private readonly IRepository<Patient> _patientRepository;
    private readonly IRepository<Consultation> _consultationRepository;
    private readonly IMapper _mapper;

    public TestService(
        IRepository<Test> testRepository,
        IRepository<Patient> patientRepository,
        IRepository<Consultation> consultationRepository,
        IMapper mapper)
    {
        _testRepository = testRepository ?? throw new ArgumentNullException(nameof(testRepository));
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
        _consultationRepository = consultationRepository ?? throw new ArgumentNullException(nameof(consultationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<TestDto> CreateTestAsync(CreateTestDto createTestDto)
    {
        var patient = await _patientRepository.GetByIdAsync(createTestDto.PatientId);
        if (patient == null)
        {
            throw new ArgumentException($"Patient with ID {createTestDto.PatientId} does not exist.");
        }

        var consultation = await _consultationRepository.GetByIdAsync(createTestDto.ConsultationId);
        if (consultation == null)
        {
            throw new ArgumentException($"Consultation with ID {createTestDto.ConsultationId} does not exist.");
        }

        var test = _mapper.Map<Test>(createTestDto);
        await _testRepository.AddAsync(test);
        return _mapper.Map<TestDto>(test);
    }

    public async Task DeleteTestAsync(int id)
    {
        var test = await _testRepository.GetByIdAsync(id);
        if (test != null)
        {
            await _testRepository.DeleteAsync(test);
        }
    }

    public async Task<IEnumerable<TestDto>> GetAllTestsAsync()
    {
        var tests = await _testRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TestDto>>(tests);
    }

    public async Task<TestDto?> GetTestByIdAsync(int id)
    {
        var test = await _testRepository.GetByIdAsync(id);
        return test == null ? null : _mapper.Map<TestDto>(test);
    }

    public async Task<IEnumerable<TestDto>> GetTestsByConsultationAsync(int consultationId)
    {
        var tests = await _testRepository.FindAsync(test => test.ConsultationId == consultationId);
        return _mapper.Map<IEnumerable<TestDto>>(tests);
    }

    public async Task<IEnumerable<TestDto>> GetTestsByPatientAsync(int patientId)
    {
        var tests = await _testRepository.FindAsync(test => test.PatientId == patientId);
        return _mapper.Map<IEnumerable<TestDto>>(tests);
    }

    public async Task<TestDto> UpdateTestAsync(UpdateTestDto updateTestDto)
    {
        var test = _mapper.Map<Test>(updateTestDto);
        await _testRepository.UpdateAsync(test);
        return _mapper.Map<TestDto>(test);
    }
}