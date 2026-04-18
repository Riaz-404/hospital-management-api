using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorsController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await _doctorService.GetAllDoctorsAsync();
        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var doctor = await _doctorService.GetDoctorByIdAsync(id);
        if (doctor == null)
        {
            return NotFound(new { message = $"Doctor with ID {id} not found." });
        }

        return Ok(doctor);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDoctorDto createDoctorDto)
    {
        try
        {
            var doctor = await _doctorService.CreateDoctorAsync(createDoctorDto);
            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctorDto updateDoctorDto)
    {
        if (id != updateDoctorDto.Id)
        {
            return BadRequest(new { message = "ID mismatch." });
        }

        var existingDoctor = await _doctorService.GetDoctorByIdAsync(id);
        if (existingDoctor == null)
        {
            return NotFound(new { message = $"Doctor with ID {id} not found." });
        }

        try
        {
            var doctor = await _doctorService.UpdateDoctorAsync(updateDoctorDto);
            return Ok(doctor);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existingDoctor = await _doctorService.GetDoctorByIdAsync(id);
        if (existingDoctor == null)
        {
            return NotFound(new { message = $"Doctor with ID {id} not found." });
        }

        try
        {
            await _doctorService.DeleteDoctorAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}