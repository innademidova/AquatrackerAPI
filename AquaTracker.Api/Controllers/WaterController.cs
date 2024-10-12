using AquaTracker.Api.Extensions;
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
        var command = new AddWaterEntryCommand(request.Amount, request.Date, request.Time);
        var result = await _mediator.Send(command);

        return this.ToActionResult(result, success => Ok(success));
    }

    [HttpPut("edit")]
    [Authorize]
    public async Task<IActionResult> EditWaterEntry(EditWaterEntryRequest request)
    {
        var command = new EditWaterEntryCommand(request.Amount, request.Id);
        var result = await _mediator.Send(command);

        return this.ToActionResult(result, success => Ok(success));
    }

    [HttpDelete("{id:int}/delete")]
    [Authorize]
    public async Task<IActionResult> DeleteWaterEntry([FromRoute] int id)
    {
        var command = new DeleteWaterEntryCommand(id);
        var result = await _mediator.Send(command);

        return this.ToActionResult(result, success => Ok(success));
    }

    [HttpGet("daily-consumption")]
    [Authorize]
    public async Task<IActionResult> GetDaiLyWaterConsumption([FromQuery] GetDaiLyWaterConsumptionRequest request)
    {
        var query = new GetDailyWaterConsumptionQuery(request.Date);
        var result = await _mediator.Send(query);

        return this.ToActionResult(result, Ok);
    }

    [HttpGet("monthly-consumption")]
    [Authorize]
    public async Task<IActionResult> GetMonthlyWaterConsumption(int year, int month)
    {
        var query = new GetMonthlyWaterConsumptionQuery(year, month);
        var result = await _mediator.Send(query);

        return this.ToActionResult(result, Ok);
    }
}