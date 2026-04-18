using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultationsController : ControllerBase
{
    private readonly IConsultationService _consultationService;

    public ConsultationsController(IConsultationService consultationService)
    {
        _consultationService = consultationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var consultations = await _consultationService.GetAllConsultationsAsync();
        return Ok(consultations);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var consultation = await _consultationService.GetConsultationByIdAsync(id);
        if (consultation == null)
        {
            return NotFound(new { message = $"Consultation with ID {id} not found." });
        }

        return Ok(consultation);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetByPatient(int patientId)
    {
        var consultations = await _consultationService.GetConsultationsByPatientAsync(patientId);
        return Ok(consultations);
    }

    [HttpGet("doctor/{doctorId}")]
    public async Task<IActionResult> GetByDoctor(int doctorId)
    {
        var consultations = await _consultationService.GetConsultationsByDoctorAsync(doctorId);
        return Ok(consultations);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateConsultationDto createConsultationDto)
    {
        try
        {
            var consultation = await _consultationService.CreateConsultationAsync(createConsultationDto);
            return CreatedAtAction(nameof(GetById), new { id = consultation.Id }, consultation);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateConsultationDto updateConsultationDto)
    {
        if (id != updateConsultationDto.Id)
        {
            return BadRequest(new { message = "ID mismatch." });
        }

        var existingConsultation = await _consultationService.GetConsultationByIdAsync(id);
        if (existingConsultation == null)
        {
            return NotFound(new { message = $"Consultation with ID {id} not found." });
        }

        try
        {
            var consultation = await _consultationService.UpdateConsultationAsync(updateConsultationDto);
            return Ok(consultation);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingConsultation = await _consultationService.GetConsultationByIdAsync(id);
        if (existingConsultation == null)
        {
            return NotFound(new { message = $"Consultation with ID {id} not found." });
        }

        try
        {
            await _consultationService.DeleteConsultationAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}