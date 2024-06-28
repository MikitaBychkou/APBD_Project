using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.RequstModels;
using Project.ResponceModels;
using Project.Services;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RevenueController(IRevenueService _revenueService) : ControllerBase
{
    [HttpPost]
    // [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> CalculateRevenue([FromBody] CalculateRevenueRequestModel model)
    {
        try
        {
            var revenue = await _revenueService.CalculateRevenueAsync(model);
            return Ok(revenue);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("calculate-expected")]
    // [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> CalculateExpectedRevenue()
    {
        try
        {
            var expectedRevenue = await _revenueService.CalculateExpectedRevenueAsync();
            return Ok(new CalculateRevenueResponseModel { Revenue = expectedRevenue });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}