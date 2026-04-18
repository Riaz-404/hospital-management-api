using System;
using HospitalManagementApi.Domain.Common;

namespace HospitalManagementApi.Domain.Entities;

public class Appointment: BaseEntity
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }

    public string Purpose { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }

    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public Consultation? Consultation { get; set; }
    public AppointmentPayment? AppointmentPayment { get; set; } = null!;
}
