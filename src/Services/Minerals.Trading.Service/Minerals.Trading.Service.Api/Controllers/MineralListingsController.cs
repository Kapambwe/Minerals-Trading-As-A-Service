using Microsoft.AspNetCore.Mvc;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MineralListingsController : ControllerBase
{
    private readonly IMineralListingManager _mineralListingManager;

    public MineralListingsController(IMineralListingManager mineralListingManager)
    {
        _mineralListingManager = mineralListingManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MineralListing>>> GetAllMineralListings()
    {
        var listings = await _mineralListingManager.GetAllMineralListingsAsync();
        return Ok(listings);
    }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<MineralListing>>> GetAvailableMineralListings()
    {
        var listings = await _mineralListingManager.GetAvailableMineralListingsAsync();
        return Ok(listings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MineralListing>> GetMineralListingById(string id)
    {
        var listing = await _mineralListingManager.GetMineralListingByIdAsync(id);
        if (listing == null)
        {
            return NotFound();
        }
        return Ok(listing);
    }

    [HttpPost]
    public async Task<ActionResult<MineralListing>> CreateMineralListing([FromBody] MineralListing listing)
    {
        var createdListing = await _mineralListingManager.CreateMineralListingAsync(listing);
        return CreatedAtAction(nameof(GetMineralListingById), new { id = createdListing.Id }, createdListing);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MineralListing>> UpdateMineralListing(string id, [FromBody] MineralListing listing)
    {
        if (id != listing.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedListing = await _mineralListingManager.UpdateMineralListingAsync(listing);
            return Ok(updatedListing);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMineralListing(string id)
    {
        var result = await _mineralListingManager.DeleteMineralListingAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult<MineralListing>> UpdateListingStatus(string id, [FromBody] string status)
    {
        try
        {
            var listing = await _mineralListingManager.UpdateListingStatusAsync(id, status);
            return Ok(listing);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
