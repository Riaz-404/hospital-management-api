
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PatientsController: ControllerBase
{
    private readonly IPatientService _patientService;
 
    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _patientService.GetAllPatientsAsync();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);
        if (patient == null)
            return NotFound(new { message = $"Patient with ID {id} not found." });

        return Ok(patient);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientDto createPatientDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var patient = await _patientService.CreatePatientAsync(createPatientDto);
        return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePatientDto updatePatientDto)
    {
        if (id != updatePatientDto.Id)
            return BadRequest(new { message = "ID mismatch." });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var patient = await _patientService.UpdatePatientAsync(updatePatientDto);
            return Ok(patient);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _patientService.DeletePatientAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
