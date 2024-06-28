using JWT.Context;
using JWT.Models;
using Microsoft.EntityFrameworkCore;
using Project.Exceptions;
using Project.RequstModels;
using Project.ResponceModels;

namespace Project.Services;

public interface IContractService
{
    Task<ContractResponseModel> GetContractByIdAsync(int id, CancellationToken cancellationToken);
    Task<ContractResponseModel> CreateContractAsync(CreateContractRequestModel model, CancellationToken cancellationToken);
}
public class ContractService(DatabaseContext _context) : IContractService
{

    public async Task<ContractResponseModel> CreateContractAsync(CreateContractRequestModel model, CancellationToken cancellationToken)
    {
        var activeContract = await _context.Contracts
            .FirstOrDefaultAsync(c => c.ClientId == model.ClientId && c.SoftwareId == model.SoftwareId && c.IsActive,cancellationToken);

        if (activeContract != null)
        {
            throw new BadRequestException("client already has an active contract for this product");
        }
        
        if ((model.EndDate - model.StartDate).TotalDays < 3 || (model.EndDate - model.StartDate).TotalDays > 30)
        {
            throw new BadRequestException("the duration of the contract must be at least 3 days and no more than 30 days");
        }
        
        var software = await _context.Softwares
            .Include(s => s.Discounts)
            .FirstOrDefaultAsync(s => s.Id == model.SoftwareId,cancellationToken);

        if (software == null)
        {
            throw new NotFoundException("software not found");
        }

        decimal discountPercentage = 0;
        var applicableDiscounts = software.Discounts
            .Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
            .OrderByDescending(d => d.Percentage)
            .ToList();

        if (applicableDiscounts.Any())
        {
            discountPercentage = applicableDiscounts.First().Percentage;
        }

        var previousContracts = await _context.Contracts
            .Where(c => c.ClientId == model.ClientId && c.IsSigned)
            .ToListAsync(cancellationToken);

        if (previousContracts.Any())
        {
            discountPercentage+=5;
        }

        var basePrice = software.Price;
        var discountedPrice = basePrice - (basePrice * discountPercentage /100);

        var additionalSupportCost = 1000 * model.AdditionalSupportYears;
        var totalPrice = discountedPrice + additionalSupportCost;

        var contract = new Contract
        {
            ClientId = model.ClientId,
            SoftwareId = model.SoftwareId,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Price = totalPrice,
            IsSigned = false,
            IsPaid = false,
            IsActive = true,
            AdditionalSupportYears = model.AdditionalSupportYears
        };

        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync(cancellationToken);

        return new ContractResponseModel
        {
            Id = contract.Id,
            ClientId = contract.ClientId,
            SoftwareId = contract.SoftwareId,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Price = contract.Price,
            IsSigned = contract.IsSigned,
            IsPaid = contract.IsPaid,
            IsActive = contract.IsActive,
            AdditionalSupportYears = contract.AdditionalSupportYears
        };
    }

    public async Task<ContractResponseModel> GetContractByIdAsync(int id, CancellationToken cancellationToken)
    {
        var contract = await _context.Contracts
            .Include(c => c.Client)
            .Include(c => c.Software)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (contract == null)
        {
            throw new NotFoundException("Contract not found");
        }

        return new ContractResponseModel
        {
            Id = contract.Id,
            ClientId = contract.ClientId,
            SoftwareId = contract.SoftwareId,
            StartDate = contract.StartDate,
            EndDate = contract.EndDate,
            Price = contract.Price,
            IsSigned = contract.IsSigned,
            IsPaid = contract.IsPaid,
            IsActive = contract.IsActive,
            AdditionalSupportYears = contract.AdditionalSupportYears
        };
    }
}