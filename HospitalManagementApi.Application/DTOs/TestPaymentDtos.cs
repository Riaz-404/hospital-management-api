using HospitalManagementApi.Domain.Entities;

namespace HospitalManagementApi.Application.DTOs;

public class CreateTestPaymentDto
{
    public ServiceType ServiceType { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int TestId { get; set; }
}

public class UpdateTestPaymentDto
{
    public int Id { get; set; }
    public ServiceType ServiceType { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string InvoiceId { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
    public int TestId { get; set; }
}

public class TestPaymentDto
{
    public int Id { get; set; }
    public ServiceType ServiceType { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string InvoiceId { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
    public int TestId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
