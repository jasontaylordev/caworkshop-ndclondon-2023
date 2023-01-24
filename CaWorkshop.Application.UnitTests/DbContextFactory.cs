using System.Data.Common;
using CaWorkshop.Application.Common.Services.Identity;
using CaWorkshop.Domain.Common;
using CaWorkshop.Infrastructure.Data;
using CaWorkshop.Infrastructure.Data.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

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

        var currentUserMock = new Mock<ICurrentUser>();
        currentUserMock.Setup(m => m.UserId)
            .Returns(Guid.Empty.ToString());

        var interceptor = 
            new AuditableEntitySaveChangesInterceptor(currentUserMock.Object);
        
        var context = new ApplicationDbContext(
            options, operationalStoreOptions, interceptor);

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
