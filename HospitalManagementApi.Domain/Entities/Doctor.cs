using HospitalManagementApi.Domain.Common;

namespace HospitalManagementApi.Domain.Entities;

public class Doctor: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; }  = string.Empty;
    public int Experience { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
}
