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
    [Authorize]
    public async Task<IActionResult> CreatePayment(CancellationToken cancellationToken,[FromBody] CreatePaymentRequestModel model)
    {
        try
        {
            var payment = await _paymentService.CreatePaymentAsync(model,cancellationToken);
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetPaymentById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id,cancellationToken);
            return Ok(payment);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}