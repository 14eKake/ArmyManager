using System.Windows;
using UI.ViewModels;

namespace UI;

public partial class MainWindow : Window
{
    public MainWindow(MainViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;         // MVVM : le VM devient DataContext
        _ = vm.LoadAsync();       // (fire-and-forget) charge la liste
    }
}
