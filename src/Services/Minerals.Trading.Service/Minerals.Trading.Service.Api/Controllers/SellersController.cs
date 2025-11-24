using Microsoft.AspNetCore.Mvc;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SellersController : ControllerBase
{
    private readonly ISellerManager _sellerManager;

    public SellersController(ISellerManager sellerManager)
    {
        _sellerManager = sellerManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Seller>>> GetAllSellers()
    {
        var sellers = await _sellerManager.GetAllSellersAsync();
        return Ok(sellers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Seller>> GetSellerById(string id)
    {
        var seller = await _sellerManager.GetSellerByIdAsync(id);
        if (seller == null)
        {
            return NotFound();
        }
        return Ok(seller);
    }

    [HttpPost]
    public async Task<ActionResult<Seller>> CreateSeller([FromBody] Seller seller)
    {
        var createdSeller = await _sellerManager.CreateSellerAsync(seller);
        return CreatedAtAction(nameof(GetSellerById), new { id = createdSeller.Id }, createdSeller);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Seller>> UpdateSeller(string id, [FromBody] Seller seller)
    {
        if (id != seller.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedSeller = await _sellerManager.UpdateSellerAsync(seller);
            return Ok(updatedSeller);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeller(string id)
    {
        var result = await _sellerManager.DeleteSellerAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{id}/approve")]
    public async Task<ActionResult<Seller>> ApproveSeller(string id)
    {
        try
        {
            var seller = await _sellerManager.ApproveSellerAsync(id);
            return Ok(seller);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}/kyc-status")]
    public async Task<ActionResult<Seller>> UpdateKYCStatus(string id, [FromBody] string kycStatus)
    {
        try
        {
            var seller = await _sellerManager.UpdateKYCStatusAsync(id, kycStatus);
            return Ok(seller);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
