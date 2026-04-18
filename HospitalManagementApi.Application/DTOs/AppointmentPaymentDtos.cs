using HospitalManagementApi.Domain.Entities;

namespace HospitalManagementApi.Application.DTOs;

public class CreateAppointmentPaymentDto
{
    public ServiceType ServiceType { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int AppointmentId { get; set; }
}

public class UpdateAppointmentPaymentDto
{
    public int Id { get; set; }
    public ServiceType ServiceType { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string InvoiceId { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
    public int AppointmentId { get; set; }
}

public class AppointmentPaymentDto
{
    public int Id { get; set; }
    public ServiceType ServiceType { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string InvoiceId { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
    public int AppointmentId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
