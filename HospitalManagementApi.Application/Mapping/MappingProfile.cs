using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Domain.Entities;

namespace HospitalManagementApi.Application.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        // Patient mappings
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<CreatePatientDto, Patient>();
        CreateMap<UpdatePatientDto, Patient>();

        // Doctor mappings
        CreateMap<Doctor, DoctorDto>().ReverseMap();
        CreateMap<CreateDoctorDto, Doctor>();
        CreateMap<UpdateDoctorDto, Doctor>();
        CreateMap<Doctor, DoctorWithDetailsDto>().ReverseMap();


        // Appointment mappings
        CreateMap<Appointment, AppointmentDto>().ReverseMap();
        CreateMap<CreateAppointmentDto, Appointment>();
        CreateMap<UpdateAppointmentDto, Appointment>();

        // Consultation mappings
        CreateMap<Consultation, ConsultationDto>();
        CreateMap<CreateConsultationDto, Consultation>();
        CreateMap<UpdateConsultationDto, Consultation>();
        CreateMap<Consultation, ConsultationWithDetailsDto>()
            .IncludeBase<Consultation, ConsultationDto>();

        // Test mappings
        CreateMap<Test, TestDto>().ReverseMap();
        CreateMap<CreateTestDto, Test>();
        CreateMap<UpdateTestDto, Test>();

        // Report mappings
        CreateMap<Report, ReportDto>().ReverseMap();
        CreateMap<CreateReportDto, Report>();
        CreateMap<UpdateReportDto, Report>();

        // Appointment Payment mappings
        CreateMap<AppointmentPayment, AppointmentPaymentDto>().ReverseMap();
        CreateMap<CreateAppointmentPaymentDto, AppointmentPayment>();
        CreateMap<UpdateAppointmentPaymentDto, AppointmentPayment>();

        // Consultation Payment mappings
        CreateMap<ConsultationPayment, ConsultationPaymentDto>().ReverseMap();
        CreateMap<CreateConsultationPaymentDto, ConsultationPayment>();
        CreateMap<UpdateConsultationPaymentDto, ConsultationPayment>();

        // Test Payment mappings
        CreateMap<TestPayment, TestPaymentDto>().ReverseMap();
        CreateMap<CreateTestPaymentDto, TestPayment>();
        CreateMap<UpdateTestPaymentDto, TestPayment>();
    }
}
