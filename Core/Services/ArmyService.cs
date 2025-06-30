using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace Core.Services;

public class ArmyService
{
    private readonly GameDbContext _db;
    private readonly int _playerId;

    public ArmyService(GameDbContext db, int playerId = 1)
    {
        _db = db;
        _playerId = playerId;
    }

    public async Task<Army> CreateArmyAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
        var army = new Army { Name = name, PlayerId = _playerId };
        _db.Armies.Add(army);
        await _db.SaveChangesAsync();
        return army;
    }

    public async Task RenameArmyAsync(int armyId, string newName)
    {
        var army = await _db.Armies.FindAsync(armyId)
            ?? throw new InvalidOperationException("Army not found");
        army.Name = newName;
        await _db.SaveChangesAsync();
    }

    public async Task RecruitUnitsAsync(int armyId, int unitTypeId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));
        var army = await _db.Armies.Include(a => a.Stacks)
            .FirstOrDefaultAsync(a => a.Id == armyId)
            ?? throw new InvalidOperationException("Army not found");
        var type = await _db.UnitTypes.AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == unitTypeId)
            ?? throw new InvalidOperationException("Unit type not found");

        // Check resources
        foreach (var pair in type.Cost)
        {
            var stock = await _db.Resources.FirstOrDefaultAsync(r => r.PlayerId == _playerId && r.Type == pair.Key);
            if (stock == null || stock.Amount < pair.Value * quantity)
                throw new InvalidOperationException("Not enough resources");
            stock.Amount -= pair.Value * quantity;
        }
        var manpower = await _db.Resources.FirstAsync(r => r.PlayerId == _playerId && r.Type == ResourceType.Manpower);
        if (manpower.Amount < type.ManpowerCost * quantity)
            throw new InvalidOperationException("Not enough manpower");
        manpower.Amount -= type.ManpowerCost * quantity;

        var stack = army.Stacks.FirstOrDefault(s => s.TypeId == unitTypeId);
        if (stack == null)
        {
            stack = new UnitStack { ArmyId = armyId, TypeId = unitTypeId, Quantity = 0 };
            _db.UnitStacks.Add(stack);
            army.Stacks.Add(stack);
        }
        stack.Quantity += quantity;

        await _db.SaveChangesAsync();
    }
}
