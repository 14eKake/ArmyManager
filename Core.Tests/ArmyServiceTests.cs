using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Models;
using Core.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Core.Tests;

public class ArmyServiceTests
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
    public async Task Create_recruit_and_rename_army()
    {
        await using var db = await CreateInMemoryDbContextAsync();
        var service = new ArmyService(db);

        // Create army
        var army = await service.CreateArmyAsync("First");
        Assert.Equal("First", army.Name);
        Assert.Single(db.Armies);

        // Recruit 3 infantry
        await service.RecruitUnitsAsync(army.Id, 1, 3);
        var stack = await db.UnitStacks.Include(s => s.Type).FirstOrDefaultAsync();
        Assert.NotNull(stack);
        Assert.Equal(3, stack!.Quantity);
        Assert.Equal("Infanterie", stack.Type.Name);

        // Rename
        await service.RenameArmyAsync(army.Id, "Renamed");
        var updatedArmy = await db.Armies.FindAsync(army.Id);
        Assert.Equal("Renamed", updatedArmy!.Name);
    }
}
