using System;
using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Repositories;

namespace HospitalManagementApi.Infrastructure.Services;

public class TestPaymentService : ITestPaymentService
{
    private readonly IRepository<TestPayment> _testPaymentRepository;
    private readonly IRepository<Test> _testRepository;
    private readonly IMapper _mapper;

    public TestPaymentService(
        IRepository<TestPayment> testPaymentRepository,
        IRepository<Test> testRepository,
        IMapper mapper)
    {
        _testPaymentRepository = testPaymentRepository ?? throw new ArgumentNullException(nameof(testPaymentRepository));
        _testRepository = testRepository ?? throw new ArgumentNullException(nameof(testRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<TestPaymentDto> CreateTestPaymentAsync(CreateTestPaymentDto createPaymentDto)
    {
        var test = await _testRepository.GetByIdAsync(createPaymentDto.TestId);
        if (test == null)
        {
            throw new ArgumentException($"Test with ID {createPaymentDto.TestId} does not exist.");
        }

        var payment = _mapper.Map<TestPayment>(createPaymentDto);
        await _testPaymentRepository.AddAsync(payment);
        return _mapper.Map<TestPaymentDto>(payment);
    }

    public async Task DeleteTestPaymentAsync(int id)
    {
        var payment = await _testPaymentRepository.GetByIdAsync(id);
        if (payment != null)
        {
            await _testPaymentRepository.DeleteAsync(payment);
        }
    }

    public async Task<IEnumerable<TestPaymentDto>> GetAllTestPaymentsAsync()
    {
        var payments = await _testPaymentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TestPaymentDto>>(payments);
    }

    public async Task<TestPaymentDto?> GetTestPaymentByIdAsync(int id)
    {
        var payment = await _testPaymentRepository.GetByIdAsync(id);
        return payment == null ? null : _mapper.Map<TestPaymentDto>(payment);
    }

    public async Task<IEnumerable<TestPaymentDto>> GetTestPaymentsByTestAsync(int testId)
    {
        var payments = await _testPaymentRepository.FindAsync(payment => payment.TestId == testId);
        return _mapper.Map<IEnumerable<TestPaymentDto>>(payments);
    }

    public async Task<TestPaymentDto> UpdateTestPaymentAsync(UpdateTestPaymentDto updatePaymentDto)
    {
        var payment = _mapper.Map<TestPayment>(updatePaymentDto);
        await _testPaymentRepository.UpdateAsync(payment);
        return _mapper.Map<TestPaymentDto>(payment);
    }
}