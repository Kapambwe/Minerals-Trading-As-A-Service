using Microsoft.AspNetCore.Mvc;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentManager _paymentManager;

    public PaymentsController(IPaymentManager paymentManager)
    {
        _paymentManager = paymentManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Payment>>> GetAllPayments()
    {
        var payments = await _paymentManager.GetAllPaymentsAsync();
        return Ok(payments);
    }

    [HttpGet("trade/{tradeId}")]
    public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentsByTradeId(string tradeId)
    {
        var payments = await _paymentManager.GetPaymentsByTradeIdAsync(tradeId);
        return Ok(payments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Payment>> GetPaymentById(string id)
    {
        var payment = await _paymentManager.GetPaymentByIdAsync(id);
        if (payment == null)
        {
            return NotFound();
        }
        return Ok(payment);
    }

    [HttpPost]
    public async Task<ActionResult<Payment>> CreatePayment([FromBody] Payment payment)
    {
        try
        {
            var createdPayment = await _paymentManager.CreatePaymentAsync(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = createdPayment.Id }, createdPayment);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Payment>> UpdatePayment(string id, [FromBody] Payment payment)
    {
        if (id != payment.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedPayment = await _paymentManager.UpdatePaymentAsync(payment);
            return Ok(updatedPayment);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(string id)
    {
        var result = await _paymentManager.DeletePaymentAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("trade/{tradeId}/total")]
    public async Task<ActionResult<decimal>> GetTotalPaymentsForTrade(string tradeId)
    {
        try
        {
            var total = await _paymentManager.GetTotalPaymentsForTradeAsync(tradeId);
            return Ok(total);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("trade/{tradeId}/fully-paid")]
    public async Task<ActionResult<bool>> IsTradeFullyPaid(string tradeId)
    {
        try
        {
            var isFullyPaid = await _paymentManager.IsTradeFullyPaidAsync(tradeId);
            return Ok(isFullyPaid);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
