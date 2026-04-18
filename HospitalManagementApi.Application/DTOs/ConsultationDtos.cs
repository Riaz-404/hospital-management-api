using System.ComponentModel.DataAnnotations;

namespace HospitalManagementApi.Application.DTOs;

public class CreateConsultationDto
{
    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string Diagnosis { get; set; } = string.Empty;

    [Required]
    public DateTime ScheduledAt { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int DoctorId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int AppointmentId { get; set; }
}

public class UpdateConsultationDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string Diagnosis { get; set; } = string.Empty;

    [Required]
    public DateTime ScheduledAt { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int DoctorId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int AppointmentId { get; set; }
}

public class ConsultationDto
{
    public int Id { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int AppointmentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class ConsultationWithDetailsDto : ConsultationDto
{
    public PatientDto? Patient { get; set; }
    public DoctorDto? Doctor { get; set; }
    public AppointmentDto? Appointment { get; set; }
    public ConsultationPaymentDto? ConsultationPayment { get; set; }
}

