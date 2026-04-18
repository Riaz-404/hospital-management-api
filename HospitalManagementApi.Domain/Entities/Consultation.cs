using HospitalManagementApi.Domain.Common;

namespace HospitalManagementApi.Domain.Entities;

public class Consultation: BaseEntity
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public int AppointmentId { get; set; }


    public string Diagnosis { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }


    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!; 
    public Appointment Appointment { get; set; } = null!;
    public ICollection<Test> Tests { get; set; } =  new List<Test>();
}
