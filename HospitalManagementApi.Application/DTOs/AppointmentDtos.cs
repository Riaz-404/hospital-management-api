namespace HospitalManagementApi.Application.DTOs;

public class CreateAppointmentDto
{
    public DateTime ScheduledAt { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
}

public class UpdateAppointmentDto
{
    public int Id { get; set; }
    public DateTime ScheduledAt { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public int PatientId { get; set; }
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