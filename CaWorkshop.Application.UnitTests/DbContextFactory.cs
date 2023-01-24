using System.Data.Common;
using CaWorkshop.Infrastructure.Data;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CaWorkshop.Application.UnitTests;

public class DbContextFactory : IDisposable
{
    private DbConnection? _connection;

    public ApplicationDbContext Create()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;

        var operationalStoreOptions = Options.Create(
            new OperationalStoreOptions());

        var context = new ApplicationDbContext(
            options, operationalStoreOptions);

        var initialiser = new ApplicationDbContextInitialiser(context);

        initialiser.Initialise();
        initialiser.Seed();

        return context;
    }

    public void Dispose()
    {
        if (_connection == null) return;

        _connection.Dispose();
        _connection = null;
    }
}
