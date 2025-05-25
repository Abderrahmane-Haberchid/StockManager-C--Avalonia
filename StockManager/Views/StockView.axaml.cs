using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StockManager.ViewModels;

namespace StockManager.Views;

public partial class StockView : UserControl
{
    public StockView()
    {
        InitializeComponent();
        DataContext = new StockViewModel();
    }
}