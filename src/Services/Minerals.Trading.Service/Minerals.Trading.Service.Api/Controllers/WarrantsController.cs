using Microsoft.AspNetCore.Mvc;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarrantsController : ControllerBase
{
    private readonly IWarrantManager _warrantManager;

    public WarrantsController(IWarrantManager warrantManager)
    {
        _warrantManager = warrantManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Warrant>>> GetAllWarrants()
    {
        var warrants = await _warrantManager.GetAllWarrantsAsync();
        return Ok(warrants);
    }

    [HttpGet("trade/{tradeId}")]
    public async Task<ActionResult<IEnumerable<Warrant>>> GetWarrantsByTradeId(string tradeId)
    {
        var warrants = await _warrantManager.GetWarrantsByTradeIdAsync(tradeId);
        return Ok(warrants);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Warrant>> GetWarrantById(string id)
    {
        var warrant = await _warrantManager.GetWarrantByIdAsync(id);
        if (warrant == null)
        {
            return NotFound();
        }
        return Ok(warrant);
    }

    [HttpPost]
    public async Task<ActionResult<Warrant>> CreateWarrant([FromBody] Warrant warrant)
    {
        var createdWarrant = await _warrantManager.CreateWarrantAsync(warrant);
        return CreatedAtAction(nameof(GetWarrantById), new { id = createdWarrant.Id }, createdWarrant);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Warrant>> UpdateWarrant(string id, [FromBody] Warrant warrant)
    {
        if (id != warrant.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedWarrant = await _warrantManager.UpdateWarrantAsync(warrant);
            return Ok(updatedWarrant);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWarrant(string id)
    {
        var result = await _warrantManager.DeleteWarrantAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{id}/transfer")]
    public async Task<ActionResult<Warrant>> TransferWarrant(string id, [FromBody] string newOwner)
    {
        try
        {
            var warrant = await _warrantManager.TransferWarrantAsync(id, newOwner);
            return Ok(warrant);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
