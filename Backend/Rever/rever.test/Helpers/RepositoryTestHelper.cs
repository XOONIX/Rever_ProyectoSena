using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using rever.contexto;

namespace rever.Tests.Helpers
{
    public static class RepositoryTestHelper
    {
        public static DatabaseService CreateInMemoryContext(string? dbName = null)
        {
            var options = new DbContextOptionsBuilder<DatabaseService>()
                .UseInMemoryDatabase(databaseName: dbName ?? Guid.NewGuid().ToString())
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var context = new DatabaseService(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
