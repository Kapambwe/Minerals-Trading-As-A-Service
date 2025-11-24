using Microsoft.AspNetCore.Mvc;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradesController : ControllerBase
{
    private readonly ITradeManager _tradeManager;

    public TradesController(ITradeManager tradeManager)
    {
        _tradeManager = tradeManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Trade>>> GetAllTrades()
    {
        var trades = await _tradeManager.GetAllTradesAsync();
        return Ok(trades);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Trade>> GetTradeById(string id)
    {
        var trade = await _tradeManager.GetTradeByIdAsync(id);
        if (trade == null)
        {
            return NotFound();
        }
        return Ok(trade);
    }

    [HttpPost]
    public async Task<ActionResult<Trade>> CreateTrade([FromBody] Trade trade)
    {
        var createdTrade = await _tradeManager.CreateTradeAsync(trade);
        return CreatedAtAction(nameof(GetTradeById), new { id = createdTrade.Id }, createdTrade);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Trade>> UpdateTrade(string id, [FromBody] Trade trade)
    {
        if (id != trade.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedTrade = await _tradeManager.UpdateTradeAsync(trade);
            return Ok(updatedTrade);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrade(string id)
    {
        var result = await _tradeManager.DeleteTradeAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{id}/novate")]
    public async Task<ActionResult<Trade>> NovateTrade(string id)
    {
        try
        {
            var trade = await _tradeManager.NovateTradeAsync(id);
            return Ok(trade);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("{id}/confirm")]
    public async Task<ActionResult<Trade>> ConfirmTrade(string id)
    {
        try
        {
            var trade = await _tradeManager.ConfirmTradeAsync(id);
            return Ok(trade);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
