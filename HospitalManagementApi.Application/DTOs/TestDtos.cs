using System.ComponentModel.DataAnnotations;

namespace HospitalManagementApi.Application.DTOs;

public class CreateTestDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string TestName { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue)]
    public int ConsultationId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }
}

public class UpdateTestDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string TestName { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue)]
    public int ConsultationId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int PatientId { get; set; }
}

public class TestDto
{
    public int Id { get; set; }
    public string TestName { get; set; } = string.Empty;
    public int ConsultationId { get; set; }
    public int PatientId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
