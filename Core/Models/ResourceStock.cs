namespace Core.Models;

/// <summary>Quantité disponible d’une ressource chez le joueur.</summary>
public class ResourceStock
{
    public int Id { get; set; }
    public ResourceType Type { get; set; }
    public int Amount { get; set; }
}
