using Microsoft.EntityFrameworkCore;
using StockManager.entitys;

namespace StockManager.dbConfig;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseNpgsql("Host=localhost;Database=avalonia_db;Username=postgres;Password=postgres");
}