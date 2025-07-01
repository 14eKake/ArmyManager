using System.Linq;
using Core.Models;

namespace UI;

public partial class UnitType
{
    public string CostDisplay => string.Join(", ",
        Cost.Select(p => $"{p.Key}: {p.Value}"));
}
