using Microsoft.AspNetCore.Mvc;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarginsController : ControllerBase
{
    private readonly IMarginManager _marginManager;

    public MarginsController(IMarginManager marginManager)
    {
        _marginManager = marginManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Margin>>> GetAllMargins()
    {
        var margins = await _marginManager.GetAllMarginsAsync();
        return Ok(margins);
    }

    [HttpGet("trade/{tradeId}")]
    public async Task<ActionResult<IEnumerable<Margin>>> GetMarginsByTradeId(string tradeId)
    {
        var margins = await _marginManager.GetMarginsByTradeIdAsync(tradeId);
        return Ok(margins);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Margin>> GetMarginById(string id)
    {
        var margin = await _marginManager.GetMarginByIdAsync(id);
        if (margin == null)
        {
            return NotFound();
        }
        return Ok(margin);
    }

    [HttpPost]
    public async Task<ActionResult<Margin>> CreateMargin([FromBody] Margin margin)
    {
        var createdMargin = await _marginManager.CreateMarginAsync(margin);
        return CreatedAtAction(nameof(GetMarginById), new { id = createdMargin.Id }, createdMargin);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Margin>> UpdateMargin(string id, [FromBody] Margin margin)
    {
        if (id != margin.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedMargin = await _marginManager.UpdateMarginAsync(margin);
            return Ok(updatedMargin);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMargin(string id)
    {
        var result = await _marginManager.DeleteMarginAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
