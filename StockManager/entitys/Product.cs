using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StockManager.entitys;

[Table("Products")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public Product(string name, double price, int quantity, string category)
    {
        Name = name;
        Category = category;
        Quantity = quantity;
        Price = price;
    }
}

