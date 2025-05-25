using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StockManager.Records;

namespace StockManager.ViewModels;

public partial class ProduitsViewModel : ViewModelBase
{
    [ObservableProperty]
    private StockViewModel _stockViewModel;
    public ProduitsViewModel(StockViewModel stockViewModel)
    {
        _stockViewModel = stockViewModel;
    }

    Product product = new Product(1, "tv sumsung", Category.TV, 11, 3400);
    Product product2 = new Product(2, "Machinw a laver", Category.MACHINELAVER, 11, 3400);
    
    [RelayCommand]
    private void AddProduct()
    {
        
        //_stockViewModel = new StockViewModel();
        Console.WriteLine("BEFORE ADDING PRODUCT =>"+_stockViewModel.Products.Count);
        StockViewModel.Products.Add(product);
        Console.WriteLine("AFTER ADDING PRODUCT 1 =>"+_stockViewModel.Products.Count);
        StockViewModel.Products.Add(product2);
        Console.WriteLine("AFTER ADDING PRODUCT 2 =>"+_stockViewModel.Products.Count);

        foreach (var p in StockViewModel.Products)
        {
            Console.WriteLine(p);
        }
    }
}