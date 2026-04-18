using System;
using HospitalManagementApi.Application.DTOs;

namespace HospitalManagementApi.Application.Service;

public interface ITestService
{
    Task<TestDto?> GetTestByIdAsync(int id);
    Task<IEnumerable<TestDto>> GetAllTestsAsync();
    Task<TestDto> CreateTestAsync(CreateTestDto createTestDto);
    Task<TestDto> UpdateTestAsync(UpdateTestDto updateTestDto);
    Task DeleteTestAsync(int id);
    Task<IEnumerable<TestDto>> GetTestsByConsultationAsync(int consultationId);
    Task<IEnumerable<TestDto>> GetTestsByPatientAsync(int patientId);
}
