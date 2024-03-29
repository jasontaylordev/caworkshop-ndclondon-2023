﻿using CaWorkshop.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace CaWorkshop.Application.Common.Services.Data;

public interface IApplicationDbContext
{
    public DbSet<TodoList> TodoLists { get; }

    public DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}