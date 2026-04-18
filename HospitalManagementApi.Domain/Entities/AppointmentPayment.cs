namespace HospitalManagementApi.Domain.Entities;

public class AppointmentPayment: Payment
{
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = null!;
}
