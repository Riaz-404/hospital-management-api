using HospitalManagementApi.Domain.Common;

namespace HospitalManagementApi.Domain.Entities;

public class Test: BaseEntity
{
    public int ConsultationId { get; set; }
    public int PatientId { get; set; }

    public string TestName { get; set; } = string.Empty;

    public Consultation Consultation { get; set; } = null!;
    public Patient Patient { get; set; } = null!;
}
