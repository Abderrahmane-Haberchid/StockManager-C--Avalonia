using System.Threading.Tasks;
using StockManager.dbConfig;
using StockManager.Dto;
using StockManager.entitys;

namespace StockManager.Models;

public class VentesService
{
    
    private readonly AppDbContext _dbContext;
    private Product product;
    
    public VentesService()
    {
        _dbContext = new AppDbContext();
    }

    public ProductDto SearchProduct(int id)
    {
        if (id == 0) return null;
        
        product = _dbContext.Products.Find(id);
        if (product == null) return null;
        
        var productDto = new ProductDto(product.Name, product.Quantity, product.Price, product.Category);

        return productDto;
    }

    public async Task AddSale(VenteDto vente)
    {
        _dbContext.Sales.AddAsync(new Sale(vente.ProductName, vente.Quantity, vente.Price, vente.Category, vente.ClientName))
            .ConfigureAwait(false);

        var newQte = product.Quantity - vente.Quantity;
        
        product.Quantity = newQte;
        
        await _dbContext.SaveChangesAsync();
        
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
    }
}