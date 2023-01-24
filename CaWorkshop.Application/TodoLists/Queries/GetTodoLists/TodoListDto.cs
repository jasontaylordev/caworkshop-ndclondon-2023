using AutoMapper;

using CaWorkshop.Application.Common.Mappings;
using CaWorkshop.Domain.Entities;

namespace CaWorkshop.Application.TodoLists.Queries.GetTodoLists;

// AutoMapper Alternative Approach
//[AutoMap(typeof(TodoList))]
public class TodoListDto : IMapFrom<TodoList>
{
    public int Id { get; set; }

    public string? Title { get; set; }
    
    public string? Colour { get; set; }

    public IList<TodoItemDto> Items { get; set; }
        = new List<TodoItemDto>();
}