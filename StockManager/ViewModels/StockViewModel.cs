 

using System.Collections.Generic;
using System.Collections.ObjectModel;
using StockManager.Records;

namespace StockManager.ViewModels;

public partial class StockViewModel : ViewModelBase
{
    public ObservableCollection<Product> Products { get; set; }
    
    public StockViewModel()
    {
        var products = new List<Product>
        {
            new Product(1, "tv sumsung", Category.TV, 11, 3400),
            new Product(1, "tv sumsung", Category.TV, 10, 1550),
            new Product(1, "tv sumsung", Category.TV, 10, 1550),
            new Product(1, "tv sumsung", Category.TV, 10, 1550),
            new Product(1, "tv sumsung", Category.TV, 10, 1550),
            new Product(1, "tv sumsung", Category.TV, 10, 1550),
            new Product(1, "tv sumsung", Category.TV, 10, 1550)
        };
            Products = new ObservableCollection<Product>(products);
    }
    
   
    
}