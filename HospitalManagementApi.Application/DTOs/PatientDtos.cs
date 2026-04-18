using System.ComponentModel.DataAnnotations;

namespace HospitalManagementApi.Application.DTOs;

public class CreatePatientDto
{
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string MobileNo { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string MedicalCondition { get; set; } = string.Empty;
}

public class UpdatePatientDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string MobileNo { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string MedicalCondition { get; set; } = string.Empty;
}

public class PatientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MedicalCondition { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

