using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.Json;

namespace Core;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

    public DbSet<ResourceStock> Resources => Set<ResourceStock>();
    public DbSet<UnitType> UnitTypes => Set<UnitType>();
    public DbSet<UnitStack> UnitStacks => Set<UnitStack>();
    public DbSet<Army> Armies => Set<Army>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        // — UnitType.Cost (Dictionary) → JSON dans SQLite —
        mb.Entity<UnitType>()
          .Property(u => u.Cost)
          .HasConversion(
              v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
              v => JsonSerializer.Deserialize<Dictionary<ResourceType, int>>(v, (JsonSerializerOptions?)null)!
          );

        // Insertion des 3 types d’unités de base
        mb.Entity<UnitType>().HasData(
            new UnitType
            {
                Id = 1,
                Name = "Infanterie",
                Attack = 10,
                Defense = 12,
                Speed = 2,
                TrainingTimeSeconds = 10,
                ManpowerCost = 1,
                Cost = new() { { ResourceType.Gold, 50 }, { ResourceType.Food, 20 }, { ResourceType.Metal, 10 } }
            },
            new UnitType
            {
                Id = 2,
                Name = "Archers",
                Attack = 14,
                Defense = 8,
                Speed = 2,
                TrainingTimeSeconds = 12,
                ManpowerCost = 1,
                Cost = new() { { ResourceType.Gold, 60 }, { ResourceType.Wood, 30 }, { ResourceType.Food, 20 } }
            },
            new UnitType
            {
                Id = 3,
                Name = "Cavalerie",
                Attack = 18,
                Defense = 14,
                Speed = 4,
                TrainingTimeSeconds = 20,
                ManpowerCost = 2,
                Cost = new() { { ResourceType.Gold, 120 }, { ResourceType.Food, 40 }, { ResourceType.Metal, 30 } }
            }
        );
    }
}
