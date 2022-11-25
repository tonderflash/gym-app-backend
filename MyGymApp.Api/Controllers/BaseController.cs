using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyGymApp.Aplication.Common.Enums;
using MyGymApp.Aplication.Common.GenericHandler;

namespace MyGymApp.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController<TBaseCommand,TResponse> : ControllerBase 
    where TBaseCommand : BaseCommand<TResponse> , new()
    where TResponse : class
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    [HttpPost]
    public virtual async Task<IActionResult> Post([FromBody] TBaseCommand command)
    {
        try
        {
            command.ActionType = ActionTypes.Create;
            return Ok(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpPut]
    public virtual async Task<IActionResult> Put([FromBody] TBaseCommand command)
    {
        try
        {
            command.ActionType = ActionTypes.Update;
            return Ok(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var command = new TBaseCommand();
            command.ActionType = ActionTypes.Delete;
            command.Id = id;
            return Ok(await Mediator.Send(command));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}