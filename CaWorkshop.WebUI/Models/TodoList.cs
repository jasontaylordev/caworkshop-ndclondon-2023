﻿using System.ComponentModel.DataAnnotations;

namespace CaWorkshop.WebUI.Models;

public class TodoList
{
    public int Id { get; set; }

    [Required]
    [StringLength(280)]
    public string Title { get; set; } = string.Empty;

    public string? Colour { get; set; }

    public IList<TodoItem> Items { get; set; }
        = new List<TodoItem>();
}
