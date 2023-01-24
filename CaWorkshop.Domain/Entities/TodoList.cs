namespace CaWorkshop.Domain.Entities;

public class TodoList : AuditableEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Colour { get; set; }

    public IList<TodoItem> Items { get; set; }
        = new List<TodoItem>();
}