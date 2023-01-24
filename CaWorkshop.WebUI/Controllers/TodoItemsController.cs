using CaWorkshop.Application.TodoItems.Commands.CreateTodoItem;
using CaWorkshop.Application.TodoItems.Commands.DeleteTodoItem;
using CaWorkshop.Application.TodoItems.Commands.UpdateTodoItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CaWorkshop.WebUI.Controllers;

public class TodoItemsController : ApiControllerBase
{
    // POST: api/TodoItems
    [HttpPost]
    public async Task<ActionResult<int>> PostTodoItem(
        CreateTodoItemCommand command)
    {
        return await Mediator.Send(command);
    }

    // PUT: api/TodoItems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(int id,
        UpdateTodoItemCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        await Mediator.Send(new DeleteTodoItemCommand { Id = id });

        return NoContent();
    }
}
