 

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using StockManager.dbConfig;
using StockManager.entitys;

namespace StockManager.ViewModels;

public partial class StockViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ObservableCollection<Product> _products = new();
    
    private AppDbContext _dbContext;
    
    public StockViewModel()
    {
       _dbContext = new AppDbContext();
        LoadProducts();    
    }

    private async void LoadProducts()
    {
       Products = new ObservableCollection<Product>(await _dbContext.Products.ToListAsync());
    }
   
    
}