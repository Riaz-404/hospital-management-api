using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestsController : ControllerBase
{
    private readonly ITestService _testService;

    public TestsController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tests = await _testService.GetAllTestsAsync();
        return Ok(tests);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var test = await _testService.GetTestByIdAsync(id);
        if (test == null)
        {
            return NotFound(new { message = $"Test with ID {id} not found." });
        }

        return Ok(test);
    }

    [HttpGet("consultation/{consultationId}")]
    public async Task<IActionResult> GetByConsultation(int consultationId)
    {
        var tests = await _testService.GetTestsByConsultationAsync(consultationId);
        return Ok(tests);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetByPatient(int patientId)
    {
        var tests = await _testService.GetTestsByPatientAsync(patientId);
        return Ok(tests);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTestDto createTestDto)
    {
        try
        {
            var test = await _testService.CreateTestAsync(createTestDto);
            return CreatedAtAction(nameof(GetById), new { id = test.Id }, test);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTestDto updateTestDto)
    {
        if (id != updateTestDto.Id)
        {
            return BadRequest(new { message = "ID mismatch." });
        }

        var existingTest = await _testService.GetTestByIdAsync(id);
        if (existingTest == null)
        {
            return NotFound(new { message = $"Test with ID {id} not found." });
        }

        try
        {
            var test = await _testService.UpdateTestAsync(updateTestDto);
            return Ok(test);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingTest = await _testService.GetTestByIdAsync(id);
        if (existingTest == null)
        {
            return NotFound(new { message = $"Test with ID {id} not found." });
        }

        await _testService.DeleteTestAsync(id);
        return NoContent();
    }
}