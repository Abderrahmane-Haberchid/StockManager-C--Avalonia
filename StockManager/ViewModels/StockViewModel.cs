 

using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using StockManager.dbConfig;
using StockManager.entitys;
namespace StockManager.ViewModels;
public partial class StockViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ObservableCollection<Product> _products = new();

    [ObservableProperty]
    private bool _successMsg;
    
    private readonly AppDbContext _dbContext;
    
    public StockViewModel()
    {
       _dbContext = new AppDbContext();
        LoadProducts();    
    }

    private async Task LoadProducts()
    {
       Products = new ObservableCollection<Product>(await _dbContext.Products.ToListAsync());
    }
    [RelayCommand]
    private async Task SaveProductUpdate(DataGridRowEditEndedEventArgs e)
    {
        if (e.Row != null && e.EditAction == DataGridEditAction.Commit && e.Row.DataContext is Product product)
        {
          await SaveRowEditChange(product);
        }
    }
    
    private async Task SaveRowEditChange(Product product)
    {
        
        if (_dbContext == null)
        {
            System.Diagnostics.Debug.WriteLine("Error: _dbContext is null in SaveRowEditChange!");
            return; 
        }

        if (product == null)
        {
            
            System.Diagnostics.Debug.WriteLine("Error: Product is null in SaveRowEditChange!");
            return; 
        }
        
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }

    [RelayCommand]
    private void ExportToCSV()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        using (var writer = new StreamWriter("products.csv"))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteHeader<Product>();
            csv.WriteRecords(_products);

            SuccessMsg = true;
        }
    }
    
}