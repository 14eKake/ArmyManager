using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Core.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Core.Tests;

public class DbContextTests
{
    private static async Task<GameDbContext> CreateInMemoryDbContextAsync()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        await connection.OpenAsync();

        var options = new DbContextOptionsBuilder<GameDbContext>()
            .UseSqlite(connection)
            .Options;

        var context = new GameDbContext(options);
        await context.Database.EnsureCreatedAsync();
        return context;
    }

    [Fact]
    public async Task Database_seed_contains_three_unit_types()
    {
        await using var db = await CreateInMemoryDbContextAsync();
        var types = await db.UnitTypes.OrderBy(u => u.Id).ToListAsync();

        Assert.Equal(3, types.Count);
        Assert.Equal(new[] { "Infanterie", "Archers", "Cavalerie" }, types.Select(t => t.Name));
    }
}
