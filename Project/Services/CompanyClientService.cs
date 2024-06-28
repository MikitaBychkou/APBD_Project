using JWT.Context;
using JWT.Models;
using Project.Exceptions;
using Project.RequstModels;
using Project.ResponceModels;

namespace Project.Services;

public interface ICompanyClientService
{
    Task<CompanyClientResponseModel> AddCompanyClientAsync(AddCompanyClientRequestModel model, CancellationToken cancellationToken);
    Task DeleteCompanyClientAsync(int id, CancellationToken cancellationToken);
    Task<CompanyClientResponseModel> UpdateCompanyClientAsync(int id, UpdateCompanyClientRequestModel model, CancellationToken cancellationToken);
    Task<CompanyClientResponseModel> GetCompanyClientByIdAsync(int id, CancellationToken cancellationToken);
}

public class CompanyClientService(DatabaseContext _context) : ICompanyClientService
{

    public async Task<CompanyClientResponseModel> AddCompanyClientAsync(AddCompanyClientRequestModel model, CancellationToken cancellationToken)
    {
        var client = new CompanyClient
        {
            CompanyName = model.CompanyName,
            KRS = model.KRS,
            Address = model.Address,
            Email = model.Email,
            Phone = model.Phone,
            ClientType = "Company",
            IsSoftDeleted = false
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync(cancellationToken);

        return new CompanyClientResponseModel
        {
            Id = client.Id,
            Address = client.Address,
            Email = client.Email,
            Phone = client.Phone,
            ClientType = client.ClientType,
            CompanyName = client.CompanyName,
            KRS = client.KRS
        };
    }

    public async Task DeleteCompanyClientAsync(int id, CancellationToken cancellationToken)
    {
        var client = await _context.Clients.FindAsync(id, cancellationToken);

        if (client == null)
        {
            throw new NotFoundException($"Client with id {id} not found");
        }

        if (client.ClientType != "Company")
        {
            throw new BadRequestException("Invalid client type");
        }

        throw new BadRequestException("Company clients cannot be deleted");
    }

    public async Task<CompanyClientResponseModel> UpdateCompanyClientAsync(int id, UpdateCompanyClientRequestModel model, CancellationToken cancellationToken)
    {
        var client = await _context.Clients.FindAsync(id,cancellationToken);

        if (client == null || client.ClientType != "Company")
        {
            throw new NotFoundException($"Client with id {id} not found");
        }

        var companyClient = client as CompanyClient;

        companyClient.CompanyName = model.CompanyName;
        companyClient.Address = model.Address;
        companyClient.Email = model.Email;
        companyClient.Phone = model.Phone;

        await _context.SaveChangesAsync(cancellationToken);

        return new CompanyClientResponseModel
        {
            Id = companyClient.Id,
            Address = companyClient.Address,
            Email = companyClient.Email,
            Phone = companyClient.Phone,
            ClientType = companyClient.ClientType,
            CompanyName = companyClient.CompanyName,
            KRS = companyClient.KRS
        };
    }

    public async Task<CompanyClientResponseModel> GetCompanyClientByIdAsync(int id, CancellationToken cancellationToken)
    {
        var client = await _context.Clients.FindAsync(id, cancellationToken);

        if (client == null || client.ClientType != "Company")
        {
            throw new NotFoundException($"Client with id {id} not found");
        }

        var companyClient = client as CompanyClient;

        return new CompanyClientResponseModel
        {
            Id = companyClient.Id,
            Address = companyClient.Address,
            Email = companyClient.Email,
            Phone = companyClient.Phone,
            ClientType = companyClient.ClientType,
            CompanyName = companyClient.CompanyName,
            KRS = companyClient.KRS
        };
    }
}