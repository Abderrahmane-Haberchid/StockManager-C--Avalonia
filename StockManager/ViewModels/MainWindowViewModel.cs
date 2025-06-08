using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace StockManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isPaneOpen = true;
    
    [ObservableProperty]
    private ViewModelBase _currentViewModel = new StockViewModel();
    
    [ObservableProperty]
    private ListItemTemplate _selectedListItem;

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;
        var instance = Activator.CreateInstance(value.ModelType);
        if (instance is null) return;
        CurrentViewModel = (ViewModelBase)instance;
    }

    public ObservableCollection<ListItemTemplate> ListItemTemplates { get; } = new()
    {
        new ListItemTemplate(typeof(StockViewModel), "stock"),
        new ListItemTemplate(typeof(ProduitsViewModel), "addProduct"),
        new ListItemTemplate(typeof(HistoryViewModel), "history"),
        new ListItemTemplate(typeof(VentesViewModel), "addSale")
    };

    [RelayCommand]
    public void OnIsPaneOpenChanged()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    [RelayCommand]
    public void Exit()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type, string iconKey)
    {
        ModelType = type;
        Label = type.Name.Replace("ViewModel", "");

        Application.Current!.TryFindResource(iconKey, out var res);
        ListItemIcon = (StreamGeometry)res!;
    }
    public string Label { get; }
    public Type ModelType { get; }
    public StreamGeometry ListItemIcon { get; }
}