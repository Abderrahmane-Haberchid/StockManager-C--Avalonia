 

using System.Collections.ObjectModel;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StockManager.Dto;
using StockManager.Models;

namespace StockManager.ViewModels;
public partial class StockViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ObservableCollection<ProductDto> _productsDto = new();

    [ObservableProperty]
    private bool _successMsg;
    
    [ObservableProperty]
    private string _query = "";
    
    // Dependecys injection start here
    private readonly StockService _stockService;
    
    public StockViewModel()
    {
        _stockService = new StockService();
        LoadProducts();  
    }

    [RelayCommand]
    private void LoadProducts()
    {
        ProductsDto.Clear();
        Query = "";
        Thread.Sleep(500);
        ProductsDto = _stockService.LoadProductsService();
    }
    
    [RelayCommand]
    private void ChercherProduit()
    {
        if (Query == "") return;
        ProductsDto.Clear();
        Thread.Sleep(1000);
        ProductsDto = _stockService.SearchProduct(Query);
        
    }
    
    [RelayCommand]
    private void ExportToCSV()
    {
        _stockService.ExportToCSV();    
        SuccessMsg = true;
        
    }

    [RelayCommand]
    private void ExportToExcel()
    {
        _stockService.ExportToExcel();
        SuccessMsg = true;
    }
    
    // [RelayCommand]
    // private async Task SaveProductUpdate(DataGridRowEditEndedEventArgs e)
    // {
    //     if (e.Row != null && e.EditAction == DataGridEditAction.Commit && e.Row.DataContext is Product product)
    //     {
    //       await SaveRowEditChange(product);
    //     }
    // }
    //
    // private async Task SaveRowEditChange(Product product)
    // {
    //     
    //     if (_dbContext == null)
    //     {
    //         System.Diagnostics.Debug.WriteLine("Error: _dbContext is null in SaveRowEditChange!");
    //         return; 
    //     }
    //
    //     if (product == null)
    //     {
    //         
    //         System.Diagnostics.Debug.WriteLine("Error: Product is null in SaveRowEditChange!");
    //         return; 
    //     }
    //     
    //     _dbContext.Products.Update(product);
    //     await _dbContext.SaveChangesAsync();
    // }

  
    
}