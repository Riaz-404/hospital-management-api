using HospitalManagementApi.Domain.Common;

namespace HospitalManagementApi.Domain.Entities;

public class Patient: BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string MobileNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MedicalCondition { get; set; } = string.Empty;

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
    public ICollection<Test> Tests { get; set; } = new List<Test>();
    public ICollection<Report> Reports { get; set; } = new List<Report>();
}
