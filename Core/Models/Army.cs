using System.Collections.Generic;

namespace Core.Models;

/// <summary>Armée du joueur (v1 = une seule armée).</summary>
public class Army
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
    public ICollection<UnitStack> Stacks { get; set; } = new List<UnitStack>();
}
