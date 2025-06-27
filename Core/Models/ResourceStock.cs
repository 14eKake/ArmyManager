using System.Collections.Generic;

namespace Core.Models;

/// <summary>Quantité disponible d’une ressource chez le joueur.</summary>
public class ResourceStock
{
    public int Id { get; set; }
    public ResourceType Type { get; set; }
    public int Amount { get; set; }

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
}
