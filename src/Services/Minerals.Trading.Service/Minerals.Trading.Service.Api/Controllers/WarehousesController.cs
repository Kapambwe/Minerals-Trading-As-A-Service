using Microsoft.AspNetCore.Mvc;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehousesController : ControllerBase
{
    private readonly IWarehouseManager _warehouseManager;

    public WarehousesController(IWarehouseManager warehouseManager)
    {
        _warehouseManager = warehouseManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Warehouse>>> GetAllWarehouses()
    {
        var warehouses = await _warehouseManager.GetAllWarehousesAsync();
        return Ok(warehouses);
    }

    [HttpGet("approved")]
    public async Task<ActionResult<IEnumerable<Warehouse>>> GetApprovedWarehouses()
    {
        var warehouses = await _warehouseManager.GetApprovedWarehousesAsync();
        return Ok(warehouses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Warehouse>> GetWarehouseById(string id)
    {
        var warehouse = await _warehouseManager.GetWarehouseByIdAsync(id);
        if (warehouse == null)
        {
            return NotFound();
        }
        return Ok(warehouse);
    }

    [HttpPost]
    public async Task<ActionResult<Warehouse>> CreateWarehouse([FromBody] Warehouse warehouse)
    {
        var createdWarehouse = await _warehouseManager.CreateWarehouseAsync(warehouse);
        return CreatedAtAction(nameof(GetWarehouseById), new { id = createdWarehouse.Id }, createdWarehouse);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Warehouse>> UpdateWarehouse(string id, [FromBody] Warehouse warehouse)
    {
        if (id != warehouse.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedWarehouse = await _warehouseManager.UpdateWarehouseAsync(warehouse);
            return Ok(updatedWarehouse);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWarehouse(string id)
    {
        var result = await _warehouseManager.DeleteWarehouseAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPost("{id}/approve")]
    public async Task<ActionResult<Warehouse>> ApproveWarehouse(string id)
    {
        try
        {
            var warehouse = await _warehouseManager.ApproveWarehouseAsync(id);
            return Ok(warehouse);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
