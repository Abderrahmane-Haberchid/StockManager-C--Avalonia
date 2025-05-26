using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StockManager.dbConfig;

namespace StockManager.ViewModels;

public partial class VentesViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _articleId;
    [ObservableProperty]
    private string _productName;
    [ObservableProperty]
    private int _quantity;
    [ObservableProperty]
    private double _price;
    [ObservableProperty]
    private string _category;
    [ObservableProperty]
    private double _totalPrice;
    [ObservableProperty]
    private string _customerName;
    
    AppDbContext _dbContext;

    public VentesViewModel()
    {
        _dbContext = new AppDbContext();

    }

    [RelayCommand]
    private void Search(int id)
    {
        var product = _dbContext.Products.Find(id);
        
        if (product == null) return;
        
        ProductName = product.Name;
        Quantity = product.Quantity;
        Price = product.Price;
        Category = product.Category;
        
        TotalPrice = Price * Quantity;
        
    }
    
}