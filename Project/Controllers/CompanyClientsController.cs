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
    public async Task<IActionResult> AddCompanyClient([FromBody] AddCompanyClientRequestModel model)
    {
        try
        {
            var result = await _companyClientService.AddCompanyClientAsync(model);
            return Created(string.Empty, null);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteCompanyClient(int id)
    {
        try
        {
            await _companyClientService.DeleteCompanyClientAsync(id);
            return NoContent();
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompanyClient(int id, [FromBody] UpdateCompanyClientRequestModel model)
    {
        try
        {
            var result = await _companyClientService.UpdateCompanyClientAsync(id, model);
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
    public async Task<IActionResult> GetCompanyClientById(int id)
    {
        try
        {
            var result = await _companyClientService.GetCompanyClientByIdAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}