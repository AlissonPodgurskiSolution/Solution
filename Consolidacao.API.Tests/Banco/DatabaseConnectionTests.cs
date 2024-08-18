using Microsoft.EntityFrameworkCore;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Consolidacao.API.Tests.Banco;

public class DatabaseConnectionTests
{
    private readonly string _connectionString = $"Server=(localdb)\\mssqllocaldb;Database=ConsolidacaoDB;Trusted_Connection=True;MultipleActiveResultSets=true";

    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
    }

    [Fact]
    public async Task ShouldConnectToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        // Act
        await using var context = new TestDbContext(options);
        var canConnect = await context.Database.CanConnectAsync();

        // Assert
        Assert.IsTrue(canConnect, "A conexão com o banco de dados deveria ser bem-sucedida.");
    }
}