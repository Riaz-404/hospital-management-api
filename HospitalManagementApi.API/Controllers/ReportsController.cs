using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var reports = await _reportService.GetAllReportsAsync();
        return Ok(reports);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var report = await _reportService.GetReportByIdAsync(id);
        if (report == null)
        {
            return NotFound(new { message = $"Report with ID {id} not found." });
        }

        return Ok(report);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetByPatient(int patientId)
    {
        var reports = await _reportService.GetReportsByPatientAsync(patientId);
        return Ok(reports);
    }

    [HttpGet("test/{testId}")]
    public async Task<IActionResult> GetByTest(int testId)
    {
        var reports = await _reportService.GetReportsByTestAsync(testId);
        return Ok(reports);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReportDto createReportDto)
    {
        try
        {
            var report = await _reportService.CreateReportAsync(createReportDto);
            return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateReportDto updateReportDto)
    {
        if (id != updateReportDto.Id)
        {
            return BadRequest(new { message = "ID mismatch." });
        }

        var existingReport = await _reportService.GetReportByIdAsync(id);
        if (existingReport == null)
        {
            return NotFound(new { message = $"Report with ID {id} not found." });
        }

        try
        {
            var report = await _reportService.UpdateReportAsync(updateReportDto);
            return Ok(report);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingReport = await _reportService.GetReportByIdAsync(id);
        if (existingReport == null)
        {
            return NotFound(new { message = $"Report with ID {id} not found." });
        }

        await _reportService.DeleteReportAsync(id);
        return NoContent();
    }
}