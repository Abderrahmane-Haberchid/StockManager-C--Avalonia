using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StockManager.ViewModels;

namespace StockManager.Views;

public partial class VentesView : UserControl
{
    public VentesView()
    {
        InitializeComponent();
        DataContext = new VentesViewModel();
    }
}