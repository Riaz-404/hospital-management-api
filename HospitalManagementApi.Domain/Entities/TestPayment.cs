using System;

namespace HospitalManagementApi.Domain.Entities;

public class TestPayment: Payment
{
    public int TestId { get; set; }
    public Test Test { get; set; } = null!;
}
