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
    public async Task<IActionResult> AddIndividualClient([FromBody] AddIndividualClientRequestModel model)
    {
        try
        {
            var result = await _individualClientService.AddIndividualClientAsync(model);
            return Created(string.Empty, null);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteIndividualClient(int id)
    {
        try
        {
            await _individualClientService.DeleteIndividualClientAsync(id);
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
    public async Task<IActionResult> UpdateIndividualClient(int id, [FromBody] UpdateIndividualClientRequestModel model)
    {
        try
        {
            var result = await _individualClientService.UpdateIndividualClientAsync(id, model);
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
    public async Task<IActionResult> GetIndividualClientById(int id)
    {
        try
        {
            var result = await _individualClientService.GetIndividualClientByIdAsync(id);
            return Ok(result);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}