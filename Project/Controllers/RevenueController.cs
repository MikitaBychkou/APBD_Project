using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.RequstModels;
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
}