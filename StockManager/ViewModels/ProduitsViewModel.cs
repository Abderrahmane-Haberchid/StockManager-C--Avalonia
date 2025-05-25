using CommunityToolkit.Mvvm.Input;
using StockManager.Records;

namespace StockManager.ViewModels;

public class ProduitsViewModel : ViewModelBase
{

    private StockViewModel _stockViewModel;
    public ProduitsViewModel(StockViewModel stockViewModel)
    {
        _stockViewModel = stockViewModel;
    }

    Product product = new Product(1, "tv sumsung", Category.TV, 11, 3400);
    
    [RelayCommand]
    public void AddProduct()
    {
        _stockViewModel.Products.Add(product);
    }
}