using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using StockManager.dbConfig;
using StockManager.Dto;
using StockManager.entitys;

namespace StockManager.Models;

public class HistoryService
{
    private readonly AppDbContext _dbContext;
    private ObservableCollection<VenteDto> _venteDtos = new();
    private List<Sale> sales;
    
    public HistoryService()
    {
        _dbContext = new AppDbContext();
        sales = new List<Sale>();
    }

    public ObservableCollection<VenteDto> LoadSales()
    {
        sales = new List<Sale>(_dbContext.Sales.ToList());

        foreach (var sale in sales)
        {
            _venteDtos.Add(new VenteDto(sale.ProductName, sale.Quantity, sale.Price, sale.Category, sale.CustomerName, sale.Date, sale.Facture));
        }

        return _venteDtos;
    }

    public void ExportExcel()
    {
        var lastrow = sales.Count;
        var realLastRow = lastrow + 1;
        var afterLastRow = lastrow + 2;
        var totalQte = 0;
        double totalPrice = 0;
        
        // Calculating quantity sales and price total
        foreach (var sale in sales)
        {
            totalQte += sale.Quantity;
            totalPrice += sale.Price * sale.Quantity;
        }
        
         using var wb = new XLWorkbook();
         var ws = wb.Worksheets.Add("SalesData");
         
         ws.FirstCell().InsertTable(sales);
         ws.Columns().Width = 30;
         
         // Quantity Column
         var range = ws.Range("C2:C"+realLastRow);
         range.AddConditionalFormat().WhenBetween(1,2).Fill.BackgroundColor = XLColor.Red;
         range.AddConditionalFormat().WhenBetween(2,4).Fill.BackgroundColor = XLColor.Orange;
         range.AddConditionalFormat().WhenBetween(4,200).Fill.BackgroundColor = XLColor.Green;
         
         ws.Cell("C" + afterLastRow).SetValue("Total: " + totalQte).Style.Font.FontSize = 16;
         ws.Cell("C"+afterLastRow).Style.Font.Bold = true;
         
         // Price Column
         var range2 = ws.Range("D2:D" + realLastRow);
         range2.AddConditionalFormat().WhenBetween(0,3000).Fill.BackgroundColor = XLColor.Red;
         range2.AddConditionalFormat().WhenBetween(3000,10000).Fill.BackgroundColor = XLColor.Orange;
         range2.AddConditionalFormat().WhenBetween(10000,999999).Fill.BackgroundColor = XLColor.Green;
         
         ws.Cell("D" + afterLastRow).SetValue("Total: " + totalPrice + " DH").Style.Font.FontSize = 16;
         ws.Cell("D"+afterLastRow).Style.Font.Bold = true;
         
         // Saving file
         wb.SaveAs("/Users/abderahman/RiderProjects/StockManager/StockManager/Export/Excel/salesData.xlsx");
    }

    public ObservableCollection<VenteDto> ChercherVente(string query)
    {
           var sales = _dbContext.Sales
                                    .Where(sale => sale.ProductName.ToLower().Contains(query.ToLower()))
                                    .ToList();
           
           if (sales.Count == 0) return null;

           foreach (var sale in sales)
           {
               _venteDtos.Add(new VenteDto(sale.ProductName, sale.Quantity, sale.Price, sale.Category, sale.CustomerName, sale.Date, sale.Facture));
           }
           return _venteDtos;
    }
}