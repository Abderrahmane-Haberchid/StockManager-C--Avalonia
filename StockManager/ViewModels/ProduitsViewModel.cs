using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StockManager.dbConfig;
using StockManager.Dto;
using StockManager.entitys;
using StockManager.Models;

namespace StockManager.ViewModels;

public partial class ProduitsViewModel : ViewModelBase
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }

    [ObservableProperty]
    private string _msg = string.Empty;
    
    [ObservableProperty]
    private string _msgColor = string.Empty;
    
    [ObservableProperty]
    private StockViewModel _stockViewModel;
    
    // Dependency injection
    private ProductDto _productDto;
    private readonly ProduitsService _produitsService;
    public ProduitsViewModel()
    {
        _stockViewModel = new StockViewModel();
        _produitsService = new ProduitsService();
    }

    
    [RelayCommand]
    private void AddProduct()
    {
        if (ProductName == "" || Quantity == 0 || Price == 0 || Category == null)
        {
            Msg = "Merci de remplir tous les champs!";
            MsgColor = "Red";
            return;
        }
        _productDto = new ProductDto(ProductName, Quantity, Price,Category);
        
       _produitsService.AddProduct(_productDto);

        Msg = "Produit ajouté au stock!";
        MsgColor = "Green";
        
    }
}