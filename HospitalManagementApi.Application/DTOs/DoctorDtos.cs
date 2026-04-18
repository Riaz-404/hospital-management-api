using System.ComponentModel.DataAnnotations;

namespace HospitalManagementApi.Application.DTOs;

public class CreateDoctorDto
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
    [StringLength(100)]
    public string Specialization { get; set; }  = string.Empty;

    [Required]
    [Range(0, 50)]
    public int Experience { get; set; }
}

public class UpdateDoctorDto
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
    [StringLength(100)]
    public string Specialization { get; set; } = string.Empty;

    [Required]
    [Range(0, 50)]
    public int Experience { get; set; }
}

public class DoctorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public int Experience { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class DoctorWithDetailsDto : DoctorDto
{
    public IEnumerable<AppointmentDto> Appointments { get; set; } = new List<AppointmentDto>();
    public IEnumerable<ConsultationDto> Consultations { get; set; } = new List<ConsultationDto>();
}

