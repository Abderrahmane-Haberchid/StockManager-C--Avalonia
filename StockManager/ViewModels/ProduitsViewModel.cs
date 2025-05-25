using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StockManager.dbConfig;
using StockManager.entitys;

namespace StockManager.ViewModels;

public partial class ProduitsViewModel : ViewModelBase
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }

    [ObservableProperty]
    private bool _successMsg;
    
    [ObservableProperty]
    private StockViewModel _stockViewModel;

    private AppDbContext _dbContext;
    public ProduitsViewModel()
    {
        _stockViewModel = new StockViewModel();
        _dbContext = new AppDbContext();
    }

    
    [RelayCommand]
    private void AddProduct()
    {
        if (ProductName == "" || Quantity == 0 || Price == 0 || Category == null) return;
        
        _dbContext.Products.Add( new Product(ProductName, Price, Quantity, Category));
        _dbContext.SaveChanges();

        Console.WriteLine("Product added" + _dbContext.SaveChanges());
        // Force UI update if needed
        OnPropertyChanged(nameof(StockViewModel));
        
        SuccessMsg = true;
    }
}