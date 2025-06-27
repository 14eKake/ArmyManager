using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Core;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace UI.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly GameDbContext _db;

    [ObservableProperty]               // génère UnitTypes + notification
    private ObservableCollection<UnitType> unitTypes = new();

    public MainViewModel(GameDbContext db)
    {
        _db = db;
    }

    /// <summary>Charge depuis la base les types d’unités (table seedée).</summary>
    public async Task LoadAsync()
    {
        var list = await _db.UnitTypes.AsNoTracking().ToListAsync();
        UnitTypes = new ObservableCollection<UnitType>(list);
    }
}
