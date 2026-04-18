using System;
using HospitalManagementApi.Application.DTOs;
namespace HospitalManagementApi.Application.Service;

public interface ITestPaymentService
{
    Task<TestPaymentDto?> GetTestPaymentByIdAsync(int id);
    Task<IEnumerable<TestPaymentDto>> GetAllTestPaymentsAsync();
    Task<TestPaymentDto> CreateTestPaymentAsync(CreateTestPaymentDto createPaymentDto);
    Task<TestPaymentDto> UpdateTestPaymentAsync(UpdateTestPaymentDto updatePaymentDto);
    Task DeleteTestPaymentAsync(int id);
    Task<IEnumerable<TestPaymentDto>> GetTestPaymentsByTestAsync(int testId);
}
