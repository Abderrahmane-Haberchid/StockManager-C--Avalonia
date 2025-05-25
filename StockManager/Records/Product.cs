namespace StockManager.Records;

public record Product(int Id, string Name, Category category, int qte, double prix);