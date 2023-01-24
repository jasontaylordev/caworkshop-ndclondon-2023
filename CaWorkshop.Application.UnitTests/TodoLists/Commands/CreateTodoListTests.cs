using CaWorkshop.Application.TodoLists.Commands.CreateTodoList;
using CaWorkshop.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CaWorkshop.Application.UnitTests.TodoLists.Commands.CreateTodoList;

public class CreateTodoListTests : IDisposable
{

    private readonly DbContextFactory _contextFactory;
    private readonly ApplicationDbContext _context;

    public CreateTodoListTests()
    {
        _contextFactory = new DbContextFactory();
        _context = _contextFactory.Create();
    }

    [Fact]
    public async Task Handle_ShouldPersistTodoList()
    {
        var command = new CreateTodoListCommand
        {
            Title = "Bucket List"
        };

        var handler = new CreateTodoListCommandHandler(_context);

        var id = await handler.Handle(command,
            CancellationToken.None);

        var entity = await _context.TodoLists
            .FirstAsync(tl => tl.Id == id);

        entity.Should().NotBeNull();
        entity.Title.Should().Be(command.Title);
    }

    public void Dispose()
    {
        _contextFactory.Dispose();
    }
}
