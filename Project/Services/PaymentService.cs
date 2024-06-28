using JWT.Context;
using JWT.Models;
using Microsoft.EntityFrameworkCore;
using Project.Exceptions;
using Project.RequstModels;
using Project.ResponceModels;

namespace Project.Services;


public interface IPaymentService
{
    Task<PaymentResponseModel> GetPaymentByIdAsync(int id,CancellationToken cancellationToken);
    Task<PaymentResponseModel> CreatePaymentAsync(CreatePaymentRequestModel model, CancellationToken cancellationToken);
}

public class PaymentService(DatabaseContext _context) : IPaymentService
{

    public async Task<PaymentResponseModel> CreatePaymentAsync(CreatePaymentRequestModel model,CancellationToken cancellationToken)
    {
        var contract = await _context.Contracts
            .FirstOrDefaultAsync(c => c.Id == model.ContractId && c.ClientId == model.ClientId,cancellationToken);

        if (contract == null)
        {
            throw new NotFoundException("Contract not found");
        }

        if (contract.IsPaid)
        {
            throw new BadRequestException("Contract is already fully paid");
        }

        if (model.PaymentDate > contract.EndDate)
        {
            throw new BadRequestException("Payment date is beyond the contract end date");
        }

        var totalPaid = await _context.Payments
            .Where(p => p.ContractId == model.ContractId)
            .SumAsync(p => p.Amount,cancellationToken);

        if (totalPaid + model.Amount > contract.Price)
        {
            throw new BadRequestException("Payment exceeds the contract price");
        }

        var payment = new Payment
        {
            ClientId = model.ClientId,
            ContractId = model.ContractId,
            Amount = model.Amount,
            PaymentDate = model.PaymentDate
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync(cancellationToken);

        if (totalPaid + model.Amount == contract.Price)
        {
            contract.IsPaid = true;
            contract.IsSigned = true;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return new PaymentResponseModel
        {
            Id = payment.Id,
            ClientId = payment.ClientId,
            ContractId = payment.ContractId,
            Amount = payment.Amount,
            PaymentDate = payment.PaymentDate
        };
    }

    public async Task<PaymentResponseModel> GetPaymentByIdAsync(int id,CancellationToken cancellationToken)
    {
        var payment = await _context.Payments
            .Include(p => p.Client)
            .Include(p => p.Contract)
            .FirstOrDefaultAsync(p => p.Id == id,cancellationToken);

        if (payment == null)
        {
            throw new NotFoundException("Payment not found");
        }

        return new PaymentResponseModel
        {
            Id = payment.Id,
            ClientId = payment.ClientId,
            ContractId = payment.ContractId,
            Amount = payment.Amount,
            PaymentDate = payment.PaymentDate
        };
    }
}