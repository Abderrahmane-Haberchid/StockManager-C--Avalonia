using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StockManager.ViewModels;

namespace StockManager.Views;

public partial class FacturesView : UserControl
{
    public FacturesView()
    {
        InitializeComponent();
        DataContext = new FacturesViewModel();
    }
}