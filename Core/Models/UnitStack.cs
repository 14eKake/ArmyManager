namespace Core.Models;

/// <summary>Un « tas » d’unités identiques (comme dans les Total War).</summary>
public class UnitStack
{
    public int Id { get; set; }
    public UnitType Type { get; set; } = null!;
    public int TypeId { get; set; }

    public int Quantity { get; set; }
}
