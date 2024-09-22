using AquaTracker.Application.Water.Commands.AddWaterEntry;
using AquaTracker.Application.Water.Commands.DeleteWaterEntry;
using AquaTracker.Application.Water.Commands.EditWaterEntry;
using AquaTracker.Application.Water.Queries.GetDailyWaterConsumption;
using AquaTracker.Application.Water.Queries.GetMonthlyWaterConsumption;
using AquaTracker.Contracts.Water.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AquaTracker.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WaterController : ControllerBase
{
    private readonly ISender _mediator;

    public WaterController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add")]
    [Authorize]
    public async Task<IActionResult> AddWaterEntry(AddWaterEntryRequest request)
    {
        var command = new AddWaterEntryCommand(request.Amount);
        var result = await _mediator.Send(command);

        return result.Match(
            success => Ok(success),
            errors => Problem());
    }

    [HttpPut("{id:int}/edit")]
    [Authorize]
    public async Task<IActionResult> EditWaterEntry(EditWaterEntryRequest request)
    {
        var command = new EditWaterEntryCommand(request.Amount, request.Id);
        var result = await _mediator.Send(command);

        return result.Match(
            success => Ok(success),
            errors => Problem());
    }

    [HttpDelete("{id:int}/delete")]
    [Authorize]
    public async Task<IActionResult> DeleteWaterEntry([FromRoute] int id)
    {
        var command = new DeleteWaterEntryCommand(id);
        var result = await _mediator.Send(command);

        return result.Match(
            success => Ok(success),
            errors => Problem());
    }
    
    [HttpGet("daily-consumption")]
    [Authorize]
    public async Task<IActionResult> GetDaiLyWaterConsumption()
    {
        var query = new GetDailyWaterConsumptionQuery();
        var result = await _mediator.Send(query);

        return result.Match(
            waterEntries => Ok(waterEntries),
            errors => Problem());
    }
    
    [HttpGet("monthly-consumption")]
    [Authorize]
    public async Task<IActionResult> GetMonthlyWaterConsumption(int year, int month)
    {
        var query = new GetMonthlyWaterConsumptionQuery(year, month);
        var result = await _mediator.Send(query);

        return result.Match(
            waterEntries => Ok(waterEntries),
            errors => Problem());
    }
}