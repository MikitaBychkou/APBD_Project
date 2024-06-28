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
    // [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractRequestModel model)
    {
        try
        {
            var contract = await _contractService.CreateContractAsync(model);
            return CreatedAtAction(nameof(GetContractById), new { id = contract.Id }, contract);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    // [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> GetContractById(int id)
    {
        try
        {
            var contract = await _contractService.GetContractByIdAsync(id);
            return Ok(contract);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}