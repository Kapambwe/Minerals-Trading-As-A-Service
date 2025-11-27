using Microsoft.AspNetCore.Mvc;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SettlementsController : ControllerBase
{
    private readonly ISettlementManager _settlementManager;

    public SettlementsController(ISettlementManager settlementManager)
    {
        _settlementManager = settlementManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Settlement>>> GetAllSettlements()
    {
        var settlements = await _settlementManager.GetAllSettlementsAsync();
        return Ok(settlements);
    }

    [HttpGet("trade/{tradeId}")]
    public async Task<ActionResult<Settlement>> GetSettlementByTradeId(string tradeId)
    {
        var settlement = await _settlementManager.GetSettlementByTradeIdAsync(tradeId);
        if (settlement == null)
        {
            return NotFound();
        }
        return Ok(settlement);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Settlement>> GetSettlementById(string id)
    {
        var settlement = await _settlementManager.GetSettlementByIdAsync(id);
        if (settlement == null)
        {
            return NotFound();
        }
        return Ok(settlement);
    }

    [HttpPost]
    public async Task<ActionResult<Settlement>> CreateSettlement([FromBody] Settlement settlement)
    {
        var createdSettlement = await _settlementManager.CreateSettlementAsync(settlement);
        return CreatedAtAction(nameof(GetSettlementById), new { id = createdSettlement.Id }, createdSettlement);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Settlement>> UpdateSettlement(string id, [FromBody] Settlement settlement)
    {
        if (id != settlement.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedSettlement = await _settlementManager.UpdateSettlementAsync(settlement);
            return Ok(updatedSettlement);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSettlement(string id)
    {
        var result = await _settlementManager.DeleteSettlementAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{id}/complete")]
    public async Task<ActionResult<Settlement>> CompleteSettlement(string id)
    {
        try
        {
            var settlement = await _settlementManager.CompleteSettlementAsync(id);
            return Ok(settlement);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("physical")]
    public async Task<ActionResult<Settlement>> ProcessPhysicalSettlement([FromBody] PhysicalSettlementRequest request)
    {
        try
        {
            var settlement = await _settlementManager.ProcessPhysicalSettlementAsync(
                request.TradeId, request.WarrantNumber, request.WarehouseLocation);
            return CreatedAtAction(nameof(GetSettlementById), new { id = settlement.Id }, settlement);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("cash")]
    public async Task<ActionResult<Settlement>> ProcessCashSettlement([FromBody] CashSettlementRequest request)
    {
        try
        {
            var settlement = await _settlementManager.ProcessCashSettlementAsync(request.TradeId, request.FinalPrice);
            return CreatedAtAction(nameof(GetSettlementById), new { id = settlement.Id }, settlement);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class PhysicalSettlementRequest
{
    public string TradeId { get; set; } = string.Empty;
    public string WarrantNumber { get; set; } = string.Empty;
    public string WarehouseLocation { get; set; } = string.Empty;
}

public class CashSettlementRequest
{
    public string TradeId { get; set; } = string.Empty;
    public decimal FinalPrice { get; set; }
}
