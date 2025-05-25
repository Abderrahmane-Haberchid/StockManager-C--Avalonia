using System;
using System.Collections.ObjectModel;
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
        new ListItemTemplate(typeof(StockViewModel)),
        new ListItemTemplate(typeof(ProduitsViewModel)),
        new ListItemTemplate(typeof(FacturesViewModel)),
        new ListItemTemplate(typeof(VentesViewModel))
    };

    [RelayCommand]
    public void OnIsPaneOpenChanged()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}

public class ListItemTemplate
{
    public ListItemTemplate(Type type)
    {
        ModelType = type;
        Label = type.Name.Replace("ViewModel", "");
    }
    public string Label { get; }
    public Type ModelType { get; }
}