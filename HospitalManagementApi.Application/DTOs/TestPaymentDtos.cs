using System.ComponentModel.DataAnnotations;
using HospitalManagementApi.Domain.Entities;

namespace HospitalManagementApi.Application.DTOs;

public class CreateTestPaymentDto
{
    [Required]
    public ServiceType ServiceType { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 10000.00)]
    public decimal Amount { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int TestId { get; set; }
}

public class UpdateTestPaymentDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public ServiceType ServiceType { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 10000.00)]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(100)]
    public string InvoiceId { get; set; } = string.Empty;

    [Required]
    public PaymentStatus Status { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
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
