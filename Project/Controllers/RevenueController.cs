using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Exceptions;
using Project.RequstModels;
using Project.ResponceModels;
using Project.Services;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevenueController(IRevenueService _revenueService) : ControllerBase
{
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CalculateRevenue(CancellationToken cancellationToken, [FromBody] CalculateRevenueRequestModel model)
    {
        try
        {
            var revenue = await _revenueService.CalculateRevenueAsync(model,cancellationToken);
            return Ok(revenue);
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("calculate-expected")]
    [Authorize]
    public async Task<IActionResult> CalculateExpectedRevenue(CancellationToken cancellationToken)
    {
        try
        {
            var expectedRevenue = await _revenueService.CalculateExpectedRevenueAsync(cancellationToken);
            return Ok(new CalculateRevenueResponseModel { Revenue = expectedRevenue });
        }
        catch (BadRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}