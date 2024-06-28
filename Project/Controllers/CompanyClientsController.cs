using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Exceptions;
using Project.RequstModels;
using Project.Services;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyClientsController(ICompanyClientService _companyClientService) : ControllerBase
{

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddCompanyClient(CancellationToken cancellationToken, [FromBody] AddCompanyClientRequestModel model)
    {
        try
        {
            var result = await _companyClientService.AddCompanyClientAsync(model ,cancellationToken);
            return Created(string.Empty, null);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateCompanyClient(int id, CancellationToken cancellationToken, [FromBody] UpdateCompanyClientRequestModel model)
    {
        try
        {
            var result = await _companyClientService.UpdateCompanyClientAsync(id, model,cancellationToken);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetCompanyClientById(int id,CancellationToken cancellationToken)
    {
        try
        {
            var result = await _companyClientService.GetCompanyClientByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}