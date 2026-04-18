using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultationPaymentsController : ControllerBase
{
    private readonly IConsultationPaymentService _consultationPaymentService;

    public ConsultationPaymentsController(IConsultationPaymentService consultationPaymentService)
    {
        _consultationPaymentService = consultationPaymentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var payments = await _consultationPaymentService.GetAllConsultationPaymentsAsync();
        return Ok(payments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var payment = await _consultationPaymentService.GetConsultationPaymentByIdAsync(id);
        if (payment == null)
        {
            return NotFound(new { message = $"Consultation payment with ID {id} not found." });
        }

        return Ok(payment);
    }

    [HttpGet("consultation/{consultationId}")]
    public async Task<IActionResult> GetByConsultation(int consultationId)
    {
        var payments = await _consultationPaymentService.GetConsultationPaymentsByConsultationAsync(consultationId);
        return Ok(payments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateConsultationPaymentDto createPaymentDto)
    {
        try
        {
            var payment = await _consultationPaymentService.CreateConsultationPaymentAsync(createPaymentDto);
            return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateConsultationPaymentDto updatePaymentDto)
    {
        if (id != updatePaymentDto.Id)
        {
            return BadRequest(new { message = "ID mismatch." });
        }

        var existingPayment = await _consultationPaymentService.GetConsultationPaymentByIdAsync(id);
        if (existingPayment == null)
        {
            return NotFound(new { message = $"Consultation payment with ID {id} not found." });
        }

        try
        {
            var payment = await _consultationPaymentService.UpdateConsultationPaymentAsync(updatePaymentDto);
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
        var existingPayment = await _consultationPaymentService.GetConsultationPaymentByIdAsync(id);
        if (existingPayment == null)
        {
            return NotFound(new { message = $"Consultation payment with ID {id} not found." });
        }

        await _consultationPaymentService.DeleteConsultationPaymentAsync(id);
        return NoContent();
    }
}