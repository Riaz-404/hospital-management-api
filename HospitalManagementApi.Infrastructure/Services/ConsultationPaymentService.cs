using System;
using AutoMapper;
using HospitalManagementApi.Application.DTOs;
using HospitalManagementApi.Application.Service;
using HospitalManagementApi.Domain.Entities;
using HospitalManagementApi.Infrastructure.Repositories;

namespace HospitalManagementApi.Infrastructure.Services;

public class ConsultationPaymentService : IConsultationPaymentService
{
    private readonly IRepository<ConsultationPayment> _consultationPaymentRepository;
    private readonly IRepository<Consultation> _consultationRepository;
    private readonly IMapper _mapper;

    public ConsultationPaymentService(
        IRepository<ConsultationPayment> consultationPaymentRepository,
        IRepository<Consultation> consultationRepository,
        IMapper mapper)
    {
        _consultationPaymentRepository = consultationPaymentRepository ?? throw new ArgumentNullException(nameof(consultationPaymentRepository));
        _consultationRepository = consultationRepository ?? throw new ArgumentNullException(nameof(consultationRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ConsultationPaymentDto> CreateConsultationPaymentAsync(CreateConsultationPaymentDto createPaymentDto)
    {
        var consultation = await _consultationRepository.GetByIdAsync(createPaymentDto.ConsultationId);
        if (consultation == null)
        {
            throw new ArgumentException($"Consultation with ID {createPaymentDto.ConsultationId} does not exist.");
        }

        var payment = _mapper.Map<ConsultationPayment>(createPaymentDto);
        await _consultationPaymentRepository.AddAsync(payment);
        return _mapper.Map<ConsultationPaymentDto>(payment);
    }

    public async Task DeleteConsultationPaymentAsync(int id)
    {
        var payment = await _consultationPaymentRepository.GetByIdAsync(id);
        if (payment != null)
        {
            await _consultationPaymentRepository.DeleteAsync(payment);
        }
    }

    public async Task<IEnumerable<ConsultationPaymentDto>> GetAllConsultationPaymentsAsync()
    {
        var payments = await _consultationPaymentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ConsultationPaymentDto>>(payments);
    }

    public async Task<ConsultationPaymentDto?> GetConsultationPaymentByIdAsync(int id)
    {
        var payment = await _consultationPaymentRepository.GetByIdAsync(id);
        return payment == null ? null : _mapper.Map<ConsultationPaymentDto>(payment);
    }

    public async Task<IEnumerable<ConsultationPaymentDto>> GetConsultationPaymentsByConsultationAsync(int consultationId)
    {
        var payments = await _consultationPaymentRepository.FindAsync(payment => payment.ConsultationId == consultationId);
        return _mapper.Map<IEnumerable<ConsultationPaymentDto>>(payments);
    }

    public async Task<ConsultationPaymentDto> UpdateConsultationPaymentAsync(UpdateConsultationPaymentDto updatePaymentDto)
    {
        var payment = _mapper.Map<ConsultationPayment>(updatePaymentDto);
        await _consultationPaymentRepository.UpdateAsync(payment);
        return _mapper.Map<ConsultationPaymentDto>(payment);
    }
}