using CaWorkshop.Application.TodoLists.Commands.CreateTodoList;
using CaWorkshop.Infrastructure.Data;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace CaWorkshop.Application.UnitTests.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommandValidatorTests : IClassFixture<TestFixture>
{
    private readonly ApplicationDbContext _context;

    public CreateTodoListCommandValidatorTests(TestFixture fixture)
    {
        _context = fixture.Context;
    }

    [Fact]
    public async Task IsValid_ShouldBeTrue_WhenListTitleIsUnique()
    {
        var command = new CreateTodoListCommand
        {
            Title = "Bucket List"
        };

        var validator = new CreateTodoListCommandValidator(_context);

        var result = await validator.TestValidateAsync(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task IsValid_ShouldBeFalse_WhenListTitleIsNotUnique()
    {
        var command = new CreateTodoListCommand
        {
            Title = "Todo List"
        };

        var validator = new CreateTodoListCommandValidator(_context);

        var result = await validator.TestValidateAsync(command);

        result.ShouldHaveValidationErrorFor(r => r.Title)
            .WithErrorCode("Unique");
    }
}
