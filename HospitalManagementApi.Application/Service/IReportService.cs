using System;
using HospitalManagementApi.Application.DTOs;

namespace HospitalManagementApi.Application.Service;

public interface IReportService
{
    Task<ReportDto?> GetReportByIdAsync(int id);
    Task<IEnumerable<ReportDto>> GetAllReportsAsync();
    Task<ReportDto> CreateReportAsync(CreateReportDto createReportDto);
    Task<ReportDto> UpdateReportAsync(UpdateReportDto updateReportDto);
    Task DeleteReportAsync(int id);
    Task<IEnumerable<ReportDto>> GetReportsByPatientAsync(int patientId);
    Task<IEnumerable<ReportDto>> GetReportsByTestAsync(int testId);
}
