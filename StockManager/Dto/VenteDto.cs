using System;

namespace StockManager.Dto;

public record VenteDto(string ProductName, int Quantity, double Price, string Category, string ClientName, DateTime Date, string facture);