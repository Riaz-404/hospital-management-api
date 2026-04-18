namespace HospitalManagementApi.Application.DTOs;

public class CreateReportDto
{
    public string Description { get; set; } = string.Empty;
    public float HealthParameter { get; set; }
    public int PatientId { get; set; }
}

public class UpdateReportDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public float HealthParameter { get; set; }
    public int PatientId { get; set; }
}

public class ReportDto
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public float HealthParameter { get; set; }
    public int PatientId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}