using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace UI.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    private readonly GameDbContext _db;

    [ObservableProperty]               // génère UnitTypes + notification
    private ObservableCollection<UnitType> unitTypes = new();

    [ObservableProperty]
    private ObservableCollection<ResourceStock> resourceStocks = new();

    [ObservableProperty]
    private ObservableCollection<Army> armies = new();

    [ObservableProperty]
    private Army? selectedArmy;

    [ObservableProperty]
    private UnitType? selectedUnitType;

    [ObservableProperty]
    private int trainingQuantity = 1;

    [ObservableProperty]
    private string newArmyName = string.Empty;

    public MainViewModel(GameDbContext db)
    {
        _db = db;
    }

    /// <summary>Charge depuis la base les données du joueur.</summary>
    [RelayCommand]
    public async Task LoadAsync()
    {
        var list = await _db.UnitTypes.AsNoTracking().ToListAsync();
        UnitTypes = new ObservableCollection<UnitType>(list);

        var res = await _db.Resources.ToListAsync();
        ResourceStocks = new ObservableCollection<ResourceStock>(res);

        var armies = await _db.Armies.Include(a => a.Stacks).ThenInclude(s => s.Type).ToListAsync();
        Armies = new ObservableCollection<Army>(armies);
        SelectedArmy = Armies.FirstOrDefault();
    }

    [RelayCommand]
    private async Task CreateArmyAsync()
    {
        if (string.IsNullOrWhiteSpace(NewArmyName)) return;
        var army = new Army { Name = NewArmyName, PlayerId = 1 };
        _db.Armies.Add(army);
        await _db.SaveChangesAsync();
        Armies.Add(army);
        NewArmyName = string.Empty;
    }

    [RelayCommand]
    private async Task TrainAsync()
    {
        if (SelectedArmy == null || SelectedUnitType == null || TrainingQuantity <= 0)
            return;

        // Vérifier les ressources
        foreach (var pair in SelectedUnitType.Cost)
        {
            var stock = ResourceStocks.FirstOrDefault(r => r.Type == pair.Key);
            if (stock == null || stock.Amount < pair.Value * TrainingQuantity)
                return;
        }
        var manpower = ResourceStocks.First(r => r.Type == ResourceType.Manpower);
        if (manpower.Amount < SelectedUnitType.ManpowerCost * TrainingQuantity)
            return;

        // Débit des ressources
        foreach (var pair in SelectedUnitType.Cost)
        {
            var stock = ResourceStocks.First(r => r.Type == pair.Key);
            stock.Amount -= pair.Value * TrainingQuantity;
        }
        manpower.Amount -= SelectedUnitType.ManpowerCost * TrainingQuantity;
        await _db.SaveChangesAsync();

        // Temps d'entraînement
        await Task.Delay(SelectedUnitType.TrainingTimeSeconds * 1000 * TrainingQuantity);

        var stack = SelectedArmy.Stacks.FirstOrDefault(s => s.TypeId == SelectedUnitType.Id);
        if (stack == null)
        {
            stack = new UnitStack { ArmyId = SelectedArmy.Id, TypeId = SelectedUnitType.Id, Quantity = 0 };
            _db.UnitStacks.Add(stack);
            SelectedArmy.Stacks.Add(stack);
        }
        stack.Quantity += TrainingQuantity;
        await _db.SaveChangesAsync();
    }
}
