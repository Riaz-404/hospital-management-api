using HospitalManagementApi.Domain.Common;

namespace HospitalManagementApi.Domain.Entities;

public class Report: BaseEntity
{
    public int TestId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int ConsultationId { get; set; }

    public decimal HealthParameter { get; set; }
    public string Description { get; set; } = string.Empty;

    public Test Test { get; set; } = null!;
    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public Consultation Consultation { get; set; } = null!;
}
