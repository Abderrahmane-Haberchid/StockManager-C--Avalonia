
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using LinqKit;
using StockManager.dbConfig;
using StockManager.Dto;
using StockManager.entitys;

namespace StockManager.Models;

public class StockService
{
    private readonly AppDbContext _dbContext;
    private ObservableCollection<ProductDto> _productDtos = new();
    public StockService()
    {
        _dbContext = new AppDbContext();
    }

    public ObservableCollection<ProductDto> LoadProductsService()
    {
        var products = new List<Product>(_dbContext.Products.ToList());

        foreach (var product in products)
        {
            _productDtos.Add(new ProductDto(product.Id, product.Name, product.Quantity, product.Price, product.Category));
        }
        
        return _productDtos;
    }
    
    public ObservableCollection<ProductDto> SearchProduct(string query)
    {
        var products = _dbContext.Products
                                    .Where(product => product.Name.ToLower().Contains(query.ToLower()))
                                    .ToList();

        foreach (var product in products)
        {
            _productDtos.Add(new ProductDto(product.Id, product.Name, product.Quantity, product.Price, product.Category));
        }
        
        return _productDtos;
    }

    public void ExportToCSV()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            Delimiter = ","
        };

        using (var writer = new StreamWriter("/Users/abderahman/RiderProjects/StockManager/StockManager/Export/Csv/products.csv"))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteHeader<Product>();
            csv.WriteRecords(_productDtos);
        }
    }

    public void ExportToExcel()
    {
        var rowCount = _productDtos.Count;
        var rangeCount = rowCount + 1;
        var lastRow = rowCount + 2;
        
        var totalStock = 0;
        _productDtos.ForEach(product =>
        {
            totalStock += product.Quantity;
        });
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Stock Manager");
            
            worksheet.Cell("A1").InsertTable(_productDtos);
            worksheet.ColumnWidth = 25;
            
            var range = worksheet.Range("C2:C"+rangeCount);
            range.AddConditionalFormat().WhenBetween(0,3).Fill.BackgroundColor = XLColor.Red;
            range.AddConditionalFormat().WhenBetween(3,6).Fill.BackgroundColor = XLColor.Orange;
            range.AddConditionalFormat().WhenBetween(6,100).Fill.BackgroundColor = XLColor.Green;

            worksheet.Cell("C" + lastRow).SetValue("Total: " + totalStock).Style.Fill.BackgroundColor = XLColor.AirForceBlue;
            worksheet.Cell("C" + lastRow).Style.Font.Bold = true;
            worksheet.Cell("C" + lastRow).Style.Font.FontSize = 16;
            worksheet.Cell("C"+ lastRow).Style.Font.FontColor = XLColor.White;
            
            workbook.SaveAs("/Users/abderahman/RiderProjects/StockManager/StockManager/Export/Excel/stock.xlsx");
        }
    }
   
}