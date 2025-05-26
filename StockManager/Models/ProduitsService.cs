using StockManager.dbConfig;
using StockManager.Dto;
using StockManager.entitys;

namespace StockManager.Models;

public class ProduitsService
{
    
    private AppDbContext _dbContext;

    public ProduitsService()
    {
        _dbContext = new AppDbContext();
    }
    
    public void AddProduct(ProductDto product)
    {
        _dbContext.Products.Add(new Product(product.ProductName, product.Price, product.Quantity, product.Category));
        _dbContext.SaveChanges();
    }
}
