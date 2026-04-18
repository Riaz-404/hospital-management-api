using HospitalManagementApi.Domain.Common;

namespace HospitalManagementApi.Domain.Entities;

public abstract class Payment: BaseEntity
{
    public ServiceType ServiceType { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string InvoiceId { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
}
