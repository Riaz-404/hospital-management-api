using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentPaymentsController : ControllerBase
{
    private readonly IAppointmentPaymentService _appointmentPaymentService;

    public AppointmentPaymentsController(IAppointmentPaymentService appointmentPaymentService)
    {
        _appointmentPaymentService = appointmentPaymentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var payments = await _appointmentPaymentService.GetAllAppointmentPaymentsAsync();
        return Ok(payments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var payment = await _appointmentPaymentService.GetAppointmentPaymentByIdAsync(id);
        if (payment == null)
        {
            return NotFound(new { message = $"Appointment payment with ID {id} not found." });
        }

        return Ok(payment);
    }

    [HttpGet("appointment/{appointmentId}")]
    public async Task<IActionResult> GetByAppointment(int appointmentId)
    {
        var payments = await _appointmentPaymentService.GetAppointmentPaymentsByAppointmentAsync(appointmentId);
        return Ok(payments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentPaymentDto createPaymentDto)
    {
        try
        {
            var payment = await _appointmentPaymentService.CreateAppointmentPaymentAsync(createPaymentDto);
            return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAppointmentPaymentDto updatePaymentDto)
    {
        if (id != updatePaymentDto.Id)
        {
            return BadRequest(new { message = "ID mismatch." });
        }

        var existingPayment = await _appointmentPaymentService.GetAppointmentPaymentByIdAsync(id);
        if (existingPayment == null)
        {
            return NotFound(new { message = $"Appointment payment with ID {id} not found." });
        }

        try
        {
            var payment = await _appointmentPaymentService.UpdateAppointmentPaymentAsync(updatePaymentDto);
            return Ok(payment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingPayment = await _appointmentPaymentService.GetAppointmentPaymentByIdAsync(id);
        if (existingPayment == null)
        {
            return NotFound(new { message = $"Appointment payment with ID {id} not found." });
        }

        await _appointmentPaymentService.DeleteAppointmentPaymentAsync(id);
        return NoContent();
    }
}