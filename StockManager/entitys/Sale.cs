using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockManager.entitys;

[Table("sales")]
public class Sale
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
    public string CustomerName { get; set; }
    public DateTime Date { get; set; }
    public string Facture { get; set; }

    public Sale(string productName, int quantity, double price, string category, string customerName, DateTime date, string facture)
    {
        ProductName = productName;
        Quantity = quantity;
        Price = price;
        Category = category;
        CustomerName = customerName;
        Date = date;
        Facture = facture;
    }
    
}