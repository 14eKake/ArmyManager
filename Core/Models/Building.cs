using System.Collections.Generic;

namespace Core.Models;

public class Building
{
    public int Id { get; set; }
    public ResourceType Produces { get; set; }
    public int ProductionRate { get; set; }   // units per minute

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
}
