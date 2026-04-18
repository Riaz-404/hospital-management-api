using System;
using HospitalManagementApi.Domain.Entities;

namespace HospitalManagementApi.Application.DTOs;

public class CreateConsultationPaymentDto
{
    public ServiceType ServiceType { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public int ConsultationId { get; set; }
}

public class UpdateConsultationPaymentDto
{
    public int Id { get; set; }
    public ServiceType ServiceType { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string InvoiceId { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
    public int ConsultationId { get; set; }
}

public class ConsultationPaymentDto
{
    public int Id { get; set; }
    public ServiceType ServiceType { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string InvoiceId { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
    public int ConsultationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
