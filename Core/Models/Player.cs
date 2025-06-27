using System.Collections.Generic;

namespace Core.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<ResourceStock> Resources { get; set; } = new List<ResourceStock>();
    public ICollection<Army> Armies { get; set; } = new List<Army>();
    public ICollection<Building> Buildings { get; set; } = new List<Building>();
}
