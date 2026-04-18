using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestPaymentsController : ControllerBase
{
    private readonly ITestPaymentService _testPaymentService;

    public TestPaymentsController(ITestPaymentService testPaymentService)
    {
        _testPaymentService = testPaymentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var payments = await _testPaymentService.GetAllTestPaymentsAsync();
        return Ok(payments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var payment = await _testPaymentService.GetTestPaymentByIdAsync(id);
        if (payment == null)
        {
            return NotFound(new { message = $"Test payment with ID {id} not found." });
        }

        return Ok(payment);
    }

    [HttpGet("test/{testId}")]
    public async Task<IActionResult> GetByTest(int testId)
    {
        var payments = await _testPaymentService.GetTestPaymentsByTestAsync(testId);
        return Ok(payments);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTestPaymentDto createPaymentDto)
    {
        try
        {
            var payment = await _testPaymentService.CreateTestPaymentAsync(createPaymentDto);
            return CreatedAtAction(nameof(GetById), new { id = payment.Id }, payment);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTestPaymentDto updatePaymentDto)
    {
        if (id != updatePaymentDto.Id)
        {
            return BadRequest(new { message = "ID mismatch." });
        }

        var existingPayment = await _testPaymentService.GetTestPaymentByIdAsync(id);
        if (existingPayment == null)
        {
            return NotFound(new { message = $"Test payment with ID {id} not found." });
        }

        try
        {
            var payment = await _testPaymentService.UpdateTestPaymentAsync(updatePaymentDto);
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
        var existingPayment = await _testPaymentService.GetTestPaymentByIdAsync(id);
        if (existingPayment == null)
        {
            return NotFound(new { message = $"Test payment with ID {id} not found." });
        }

        await _testPaymentService.DeleteTestPaymentAsync(id);
        return NoContent();
    }
}