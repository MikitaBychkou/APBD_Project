using JWT.Context;
using JWT.Models;
using Project.Exceptions;
using Project.RequstModels;
using Project.ResponceModels;

namespace Project.Services;

public interface IIndividualClientService
{
    Task<IndividualClientResponseModel> AddIndividualClientAsync(AddIndividualClientRequestModel model);
    Task DeleteIndividualClientAsync(int id);
    Task<IndividualClientResponseModel> UpdateIndividualClientAsync(int id, UpdateIndividualClientRequestModel model);
    Task<IndividualClientResponseModel> GetIndividualClientByIdAsync(int id);
}

public class IndividualClientService(DatabaseContext _context) : IIndividualClientService
{

    public async Task<IndividualClientResponseModel> AddIndividualClientAsync(AddIndividualClientRequestModel model)
    {
        var client = new IndividualClient
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            PESEL = model.PESEL,
            Address = model.Address,
            Email = model.Email,
            Phone = model.Phone,
            ClientType = "Individual",
            IsSoftDeleted = false
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return new IndividualClientResponseModel
        {
            Id = client.Id,
            Address = client.Address,
            Email = client.Email,
            Phone = client.Phone,
            ClientType = client.ClientType,
            FirstName = client.FirstName,
            LastName = client.LastName,
            PESEL = client.PESEL
        };
    }

    public async Task DeleteIndividualClientAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            throw new NotFoundException($"Client with id {id} not found");
        }

        if (client.ClientType != "Individual")
        {
            throw new BadRequestException("Invalid client type");
        }

        client.IsSoftDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<IndividualClientResponseModel> UpdateIndividualClientAsync(int id, UpdateIndividualClientRequestModel model)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null || client.ClientType != "Individual")
        {
            throw new NotFoundException($"Client with id {id} not found");
        }

        var individualClient = client as IndividualClient;

        individualClient.FirstName = model.FirstName;
        individualClient.LastName = model.LastName;
        individualClient.Address = model.Address;
        individualClient.Email = model.Email;
        individualClient.Phone = model.Phone;

        await _context.SaveChangesAsync();

        return new IndividualClientResponseModel
        {
            Id = individualClient.Id,
            Address = individualClient.Address,
            Email = individualClient.Email,
            Phone = individualClient.Phone,
            ClientType = individualClient.ClientType,
            FirstName = individualClient.FirstName,
            LastName = individualClient.LastName,
            PESEL = individualClient.PESEL
        };
    }

    public async Task<IndividualClientResponseModel> GetIndividualClientByIdAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null || client.ClientType != "Individual")
        {
            throw new NotFoundException($"Client with id {id} not found");
        }

        var individualClient = client as IndividualClient;

        return new IndividualClientResponseModel
        {
            Id = individualClient.Id,
            Address = individualClient.Address,
            Email = individualClient.Email,
            Phone = individualClient.Phone,
            ClientType = individualClient.ClientType,
            FirstName = individualClient.FirstName,
            LastName = individualClient.LastName,
            PESEL = individualClient.PESEL
        };
    }
}