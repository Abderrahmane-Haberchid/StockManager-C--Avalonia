using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StockManager.ViewModels;

namespace StockManager.Views;

public partial class ProduitsView : UserControl
{
    public ProduitsView()
    {
        InitializeComponent();
        DataContext = new ProduitsViewModel();
    }
}