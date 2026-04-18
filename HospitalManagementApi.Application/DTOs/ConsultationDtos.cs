namespace HospitalManagementApi.Application.DTOs;

public class CreateConsultationDto
{
    public string Diagnoses { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int AppointmentId { get; set; }
}

public class UpdateConsultationDto
{
    public int Id { get; set; }
    public string Diagnoses { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int AppointmentId { get; set; }
}

public class ConsultationDto
{
    public int Id { get; set; }
    public string Diagnoses { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int AppointmentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

