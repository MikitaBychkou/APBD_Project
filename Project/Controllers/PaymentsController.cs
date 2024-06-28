using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.RequstModels;
using Project.Services;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(IPaymentService _paymentService) : ControllerBase
{
    
    [HttpPost]
    // [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequestModel model)
    {
        try
        {
            var payment = await _paymentService.CreatePaymentAsync(model);
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    // [Authorize(Roles = "admin, user")]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        try
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            return Ok(payment);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}