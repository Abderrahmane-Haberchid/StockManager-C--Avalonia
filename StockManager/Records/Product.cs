using CommunityToolkit.Mvvm.ComponentModel;

namespace StockManager.Records;

public partial class Product : ObservableObject
{
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string _name = string.Empty;
    
    [ObservableProperty]
    private string _category;
    
    [ObservableProperty]
    private int _qte;
    
    [ObservableProperty]
    private double _prix;

    public Product(int id, string name, string category, int qte, double prix)
    {
        Id = id;
        Name = name;
        Category = category;
        Qte = qte;
        Prix = prix;
    }
    public Product(){}
}