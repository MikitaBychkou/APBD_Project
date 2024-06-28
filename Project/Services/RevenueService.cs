﻿using JWT.Context;
using Microsoft.EntityFrameworkCore;
using Project.RequstModels;
using Project.ResponceModels;

namespace Project.Services;

public interface IRevenueService
{
    Task<RevenueResponseModel> CalculateRevenueAsync(CalculateRevenueRequestModel model);
}
public class RevenueService(DatabaseContext _context) : IRevenueService
{

    public async Task<RevenueResponseModel> CalculateRevenueAsync(CalculateRevenueRequestModel model)
    {
        decimal totalRevenue = 0;

        if (model.SoftwareId.HasValue)
        {
            var contracts = await _context.Contracts
                .Where(c => c.SoftwareId == model.SoftwareId.Value && c.IsSigned && c.IsPaid)
                .ToListAsync();

            totalRevenue = contracts.Sum(c => c.Price);
        }
        else
        {
            var contracts = await _context.Contracts
                .Where(c => c.IsSigned && c.IsPaid)
                .ToListAsync();

            totalRevenue = contracts.Sum(c => c.Price);
        }

        return new RevenueResponseModel
        {
            TotalRevenue = totalRevenue,
            TotalRevenueInPLN = totalRevenue,
            Currency = "PLN"
        };
    }
}