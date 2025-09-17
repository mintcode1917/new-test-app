using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApp.Infrastructure.Commands.CreateTaskCommand;

namespace TestApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // [HttpGet]
    // public async Task<IActionResult> GetTasks()
    // {
    //     var query = new GetAllTasksQuery();
    //     var result = await _mediator.Send(query);
    //     return Ok(result);
    // }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetTask(int id)
    // {
    //     var query = new GetTaskByIdQuery(id);
    //     var result = await _mediator.Send(query);
    //     return result != null ? Ok(result) : NotFound();
    // }

    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskCommand command)
    {
        var taskId = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateTask), new { id = taskId }, taskId);
    }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateTask(int id, UpdateTaskCommand command)
    // {
    //     if (id != command.Id)
    //     {
    //         return BadRequest();
    //     }
    //
    //     await _mediator.Send(command);
    //     return NoContent();
    // }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteTask(int id)
    // {
    //     var command = new DeleteTaskCommand(id);
    //     await _mediator.Send(command);
    //     return NoContent();
    // }
}