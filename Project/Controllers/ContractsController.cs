using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Exceptions;
using Project.RequstModels;
using Project.Services;

namespace Project.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ContractsController(IContractService _contractService) : ControllerBase
{

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateContract(CancellationToken cancellationToken,[FromBody] CreateContractRequestModel model)
    {
        try
        {
            var contract = await _contractService.CreateContractAsync(model,cancellationToken);
            return CreatedAtAction(nameof(GetContractById), new { id = contract.Id }, contract);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetContractById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var contract = await _contractService.GetContractByIdAsync(id,cancellationToken);
            return Ok(contract);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}