using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Exceptions;
using Project.RequstModels;
using Project.Services;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IndividualClientsController(IIndividualClientService _individualClientService) : ControllerBase
{

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddIndividualClient(CancellationToken cancellationToken, [FromBody] AddIndividualClientRequestModel model)
    {
        try
        {
            var result = await _individualClientService.AddIndividualClientAsync(model,cancellationToken);
            return Created(string.Empty, null);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteIndividualClient(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _individualClientService.DeleteIndividualClientAsync(id,cancellationToken);
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
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateIndividualClient(int id, CancellationToken cancellationToken,[FromBody] UpdateIndividualClientRequestModel model)
    {
        try
        {
            var result = await _individualClientService.UpdateIndividualClientAsync(id, model,cancellationToken);
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
    public async Task<IActionResult> GetIndividualClientById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _individualClientService.GetIndividualClientByIdAsync(id,cancellationToken);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}