using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Application.Common.Models;
using CaWorkshop.Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace CaWorkshop.Application.TodoLists.Queries.GetTodoLists;

public class GetTodoListsQuery : IRequest<TodosVm>
{
    //public int PageNumber { get; set; }
    //public int PageSize { get; set; }
    //public string TitleFilter { get; set; }
}

public class GetTodoListsQueryHandler : IRequestHandler<GetTodoListsQuery, TodosVm>
{
    private readonly IApplicationDbContext _context;

    public GetTodoListsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TodosVm> Handle(GetTodoListsQuery request, CancellationToken cancellationToken)
    {
        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                        .Cast<PriorityLevel>()
                        .Select(p => new LookupDto
                        {
                            Value = (int)p,
                            Name = p.ToString()
                        })
                        .ToList(),

            Lists = await _context.TodoLists
                        .Select(TodoListDto.Projection)
                        .ToListAsync(cancellationToken)
        };
    }
}