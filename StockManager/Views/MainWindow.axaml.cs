using Avalonia.Controls;
using StockManager.ViewModels;

namespace StockManager.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}