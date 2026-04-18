using System.ComponentModel.DataAnnotations;

namespace HospitalManagementApi.Application.DTOs;

public class CreateReportDto
{
    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public float HealthParameter { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int TestId { get; set; }
}

public class UpdateReportDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public float HealthParameter { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int TestId { get; set; }
}

public class ReportDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public float HealthParameter { get; set; }
    public int PatientId { get; set; }
    public int TestId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}