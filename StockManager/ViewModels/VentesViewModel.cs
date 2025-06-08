
using System;
using System.Security.Cryptography;
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
    private string _customerName;
    
    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty] 
    private string _MessageColor = string.Empty;
    
    [ObservableProperty]
    private bool _printInvoice;
    
    [ObservableProperty]
    private string printingPdf = String.Empty;
    
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
        Price = productDto.Price;
        Category = productDto.Category;
    }
    [RelayCommand]
    private async Task AddSale()
    {
        if (ProductName == "" || Quantity <= 0 || Category == "" || CustomerName == "")
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
        
        await _ventesService.AddSale(
            new VenteDto(ProductName, Quantity, Price, Category, CustomerName, 
                new DateTime(), RandomNumberGenerator.GetInt32(1, 1000)+"_"+ProductName+"_"+CustomerName));
        
        Message = "Vente validé avec succes!";
        MessageColor = "Green";

        PrintInvoice = true;

    }
    [RelayCommand]
    private void PrintPDF()
    {
        PrintingPdf = "Chargement du Facture PDF...";
        _ventesService.GenerateInvoicePdf(new VenteDto(ProductName, Quantity, Price, Category, CustomerName, new DateTime(), RandomNumberGenerator.GetInt32(1, 1000)+"_"+ProductName+"_"+CustomerName));
        PrintingPdf = "Facture PDF chargée avec succes!";
    }

    [RelayCommand]
    private void PrintExcel()
    {
        _ventesService.PrintExcel(new VenteDto(ProductName, Quantity, Price, Category, CustomerName, new DateTime(), RandomNumberGenerator.GetInt32(1, 1000)+"_"+ProductName+"_"+CustomerName));
        PrintingPdf = "Facture Ecel chargée avec succes!";
    }
    
}