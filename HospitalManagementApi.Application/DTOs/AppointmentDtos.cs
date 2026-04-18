using System.ComponentModel.DataAnnotations;

namespace HospitalManagementApi.Application.DTOs;

public class CreateAppointmentDto
{
    [Required]
    public DateTime ScheduledAt { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Purpose { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int DoctorId { get; set; }
}

public class UpdateAppointmentDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime ScheduledAt { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Purpose { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int DoctorId { get; set; }
}

public class AppointmentDto
{
    public int Id { get; set; }
    public DateTime ScheduledAt { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}