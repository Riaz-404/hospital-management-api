using System;

namespace HospitalManagementApi.Application.DTOs;

public class CreateTestDto
{
    public string TestName { get; set; } = string.Empty;
    public int ConsultationId { get; set; }
    public int PatientId { get; set; }
}

public class UpdateTestDto
{
    public int Id { get; set; }
    public string TestName { get; set; } = string.Empty;
    public int ConsultationId { get; set; }
    public int PatientId { get; set; }
}

public class TestDto
{
    public int Id { get; set; }
    public string TestName { get; set; } = string.Empty;
    public int ConsultationId { get; set; }
    public int PatientId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
