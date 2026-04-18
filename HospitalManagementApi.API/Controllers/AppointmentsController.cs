using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _appointmentService.GetAllAppointmentsAsync();
        return Ok(appointments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound(new { message = $"Appointment with ID {id} not found." });
        }

        return Ok(appointment);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetByPatient(int patientId)
    {
        var appointments = await _appointmentService.GetAppointmentsByPatientAsync(patientId);
        return Ok(appointments);
    }

    [HttpGet("doctor/{doctorId}")]
    public async Task<IActionResult> GetByDoctor(int doctorId)
    {
        var appointments = await _appointmentService.GetAppointmentsByDoctorAsync(doctorId);
        return Ok(appointments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentDto createAppointmentDto)
    {
        try
        {
            var appointment = await _appointmentService.CreateAppointmentAsync(createAppointmentDto);
            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAppointmentDto updateAppointmentDto)
    {
        if (id != updateAppointmentDto.Id)
        {
            return BadRequest(new { message = "ID mismatch." });
        }

        var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (existingAppointment == null)
        {
            return NotFound(new { message = $"Appointment with ID {id} not found." });
        }

        try
        {
            var appointment = await _appointmentService.UpdateAppointmentAsync(updateAppointmentDto);
            return Ok(appointment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingAppointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (existingAppointment == null)
        {
            return NotFound(new { message = $"Appointment with ID {id} not found." });
        }

        await _appointmentService.DeleteAppointmentAsync(id);
        return NoContent();
    }
}