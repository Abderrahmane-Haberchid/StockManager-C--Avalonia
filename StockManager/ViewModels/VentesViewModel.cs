
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StockManager.Dto;
using StockManager.Models;

namespace StockManager.ViewModels;

public partial class VentesViewModel : ViewModelBase
{
    [ObservableProperty]
    private int _articleId;
    [ObservableProperty]
    private string _productName = string.Empty;
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
    
    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty] 
    private string _MessageColor = string.Empty;
    
    // Dependecy injection start here
    private readonly VentesService _ventesService;
    private ProductDto productDto;
    
    public VentesViewModel()
    {
        _ventesService = new VentesService();

    }

    [RelayCommand]
    private void Search(int id)
    {
        productDto = _ventesService.SearchProduct(id);
        
        ProductName = productDto.ProductName;
        Quantity = productDto.Quantity;
        Price = productDto.Price;
        Category = productDto.Category;
        
        TotalPrice = Price * Quantity;
        
    }
    [RelayCommand]
    private async Task AddSale()
    {
        if (ProductName == "" || Quantity <= 0 || Price == 0 || Category == "" || CustomerName == "")
        {
            Message = "Merci de remplir tous les champs";
            MessageColor = "Red";
            return;
        }
        if (Quantity > productDto.Quantity)
        {
            Message = "Quantité insuffisante!";
            MessageColor = "Red";
            return;
        }
        
        _ventesService.AddSale(new VenteDto(ProductName, Quantity, TotalPrice, Category, CustomerName));
        
        Message = "Vente validé avec succes!";
        MessageColor = "Green";
        
    }
    
}