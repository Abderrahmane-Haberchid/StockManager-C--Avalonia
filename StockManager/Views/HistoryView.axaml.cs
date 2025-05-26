using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using StockManager.ViewModels;

namespace StockManager.Views;

public partial class HistoryView : UserControl
{
    public HistoryView()
    {
        InitializeComponent();
        DataContext = new HistoryViewModel();
    }
}