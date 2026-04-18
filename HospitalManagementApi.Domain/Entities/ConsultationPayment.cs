namespace HospitalManagementApi.Domain.Entities;

public class ConsultationPayment: Payment
{
    public int ConsultationId { get; set; }
    public Consultation Consultation { get; set; } = null!;
}
