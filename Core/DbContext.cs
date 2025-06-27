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
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Building> Buildings => Set<Building>();

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

        // Player de base avec quelques ressources
        mb.Entity<Player>().HasData(new Player { Id = 1, Name = "Player" });
        mb.Entity<ResourceStock>().HasData(
            new { Id = 1, PlayerId = 1, Type = ResourceType.Gold, Amount = 500 },
            new { Id = 2, PlayerId = 1, Type = ResourceType.Food, Amount = 300 },
            new { Id = 3, PlayerId = 1, Type = ResourceType.Metal, Amount = 200 },
            new { Id = 4, PlayerId = 1, Type = ResourceType.Manpower, Amount = 50 }
        );
    }
}
