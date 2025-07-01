using System.Collections.Generic;

namespace Core.Models;

/// <summary>Gabarit d’un type d’unité : coût, stats de base…</summary>
public partial class UnitType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    /// <summary>Coût initial de recrutement, hors Manpower.</summary>
    public Dictionary<ResourceType, int> Cost { get; set; } = new();

    /// <summary>Nombre de points de Manpower requis par unité.</summary>
    public int ManpowerCost { get; set; }

    public float Attack { get; set; }
    public float Defense { get; set; }
    public float Speed { get; set; }

    /// <summary>Temps d’entraînement (en secondes) pour UNE unité.</summary>
    public int TrainingTimeSeconds { get; set; }
}
