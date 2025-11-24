using Microsoft.AspNetCore.Mvc;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InspectionsController : ControllerBase
{
    private readonly IInspectionManager _inspectionManager;

    public InspectionsController(IInspectionManager inspectionManager)
    {
        _inspectionManager = inspectionManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inspection>>> GetAllInspections()
    {
        var inspections = await _inspectionManager.GetAllInspectionsAsync();
        return Ok(inspections);
    }

    [HttpGet("warehouse/{warehouseId}")]
    public async Task<ActionResult<IEnumerable<Inspection>>> GetInspectionsByWarehouseId(string warehouseId)
    {
        var inspections = await _inspectionManager.GetInspectionsByWarehouseIdAsync(warehouseId);
        return Ok(inspections);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Inspection>> GetInspectionById(string id)
    {
        var inspection = await _inspectionManager.GetInspectionByIdAsync(id);
        if (inspection == null)
        {
            return NotFound();
        }
        return Ok(inspection);
    }

    [HttpPost]
    public async Task<ActionResult<Inspection>> CreateInspection([FromBody] Inspection inspection)
    {
        var createdInspection = await _inspectionManager.CreateInspectionAsync(inspection);
        return CreatedAtAction(nameof(GetInspectionById), new { id = createdInspection.Id }, createdInspection);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Inspection>> UpdateInspection(string id, [FromBody] Inspection inspection)
    {
        if (id != inspection.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedInspection = await _inspectionManager.UpdateInspectionAsync(inspection);
            return Ok(updatedInspection);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInspection(string id)
    {
        var result = await _inspectionManager.DeleteInspectionAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
