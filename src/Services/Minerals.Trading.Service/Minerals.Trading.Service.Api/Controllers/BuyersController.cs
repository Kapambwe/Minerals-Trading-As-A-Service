using Microsoft.AspNetCore.Mvc;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuyersController : ControllerBase
{
    private readonly IBuyerManager _buyerManager;

    public BuyersController(IBuyerManager buyerManager)
    {
        _buyerManager = buyerManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Buyer>>> GetAllBuyers()
    {
        var buyers = await _buyerManager.GetAllBuyersAsync();
        return Ok(buyers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Buyer>> GetBuyerById(string id)
    {
        var buyer = await _buyerManager.GetBuyerByIdAsync(id);
        if (buyer == null)
        {
            return NotFound();
        }
        return Ok(buyer);
    }

    [HttpPost]
    public async Task<ActionResult<Buyer>> CreateBuyer([FromBody] Buyer buyer)
    {
        var createdBuyer = await _buyerManager.CreateBuyerAsync(buyer);
        return CreatedAtAction(nameof(GetBuyerById), new { id = createdBuyer.Id }, createdBuyer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Buyer>> UpdateBuyer(string id, [FromBody] Buyer buyer)
    {
        if (id != buyer.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedBuyer = await _buyerManager.UpdateBuyerAsync(buyer);
            return Ok(updatedBuyer);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBuyer(string id)
    {
        var result = await _buyerManager.DeleteBuyerAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{id}/approve")]
    public async Task<ActionResult<Buyer>> ApproveBuyer(string id)
    {
        try
        {
            var buyer = await _buyerManager.ApproveBuyerAsync(id);
            return Ok(buyer);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}/kyc-status")]
    public async Task<ActionResult<Buyer>> UpdateKYCStatus(string id, [FromBody] string kycStatus)
    {
        try
        {
            var buyer = await _buyerManager.UpdateKYCStatusAsync(id, kycStatus);
            return Ok(buyer);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
