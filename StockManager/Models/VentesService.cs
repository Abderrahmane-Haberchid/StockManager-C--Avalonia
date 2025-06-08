
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Avalonia.Media;
using ClosedXML.Excel;
using IronPdf;
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
        
        var productDto = new ProductDto(product.Id, product.Name, product.Quantity, product.Price, product.Category);

        return productDto;
    }

    public async Task AddSale(VenteDto vente)
    {
        
       await _dbContext.Sales.AddAsync(new Sale(vente.ProductName, vente.Quantity, vente.Price, vente.Category, vente.ClientName, vente.Date, vente.facture))
            .ConfigureAwait(false);

        var newQte = product.Quantity - vente.Quantity;
        
        product.Quantity = newQte;
        
        await _dbContext.SaveChangesAsync();
        
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync();
        
        
    }

    public void GenerateInvoicePdf(VenteDto venteDto)
    {
        double total = venteDto.Price * venteDto.Quantity;
        var html = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Invoice</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            margin: 40px;
            color: #333;
        }}
        .container {{
            max-width: 800px;
            margin: 0 auto;
            border: 1px solid #eee;
            padding: 30px;
            box-shadow: 0 0 10px rgba(0,0,0,0.05);
        }}
        .header {{
            text-align: center;
            margin-bottom: 30px;
        }}
        .header h1 {{
            color: #3498DB;
            margin-bottom: 5px;
        }}
        .header p {{
            font-size: 14px;
            color: #777;
        }}
        .customer-info {{
            margin-bottom: 30px;
            padding: 10px;
            border: 1px solid #eee;
            background-color: #f9f9f9;
        }}
        .customer-info p {{
            margin: 0;
            line-height: 1.6;
        }}
        .sales-table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 30px;
        }}
        .sales-table th, .sales-table td {{
            border: 1px solid #ddd;
            padding: 10px;
            text-align: left;
        }}
        .sales-table th {{
            background-color: #f2f2f2;
            font-weight: bold;
        }}
        .sales-table tfoot td {{
            font-weight: bold;
            background-color: #f2f2f2;
            text-align: right;
        }}
        .sales-table tfoot .total-label {{
            text-align: left;
        }}
        .footer {{
            text-align: center;
            font-size: 12px;
            color: #aaa;
            margin-top: 40px;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>StockManager - Invoice</h1>
            <p>Date of Edition: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}</p>
        </div>

        <div class=""customer-info"">
            <p><strong>Customer Name:</strong> {venteDto.ClientName.ToUpper()}</p>
            </div>

        <table class=""sales-table"">
            <thead>
                <tr>
                    <th>Product Name</th>
                    <th>Quantity</th>
                    <th>Unit Price</th> 
                    <th>Total Price</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>{venteDto.ProductName}</td>
                    <td>{venteDto.Quantity}</td>
                    <td>{venteDto.Price:C}</td>  
                    <td>{total:C}</td>
                </tr>
                </tbody>
           
        </table>

        <div class=""footer"">
            Thank you for your business!
        </div>
    </div>
</body>
</html>";
        string fullpath = Path.Combine("/Users/abderahman/RiderProjects/StockManager/StockManager/Export/PdfInvoice",
            "Invoice_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "_" + venteDto.ProductName + "_" + venteDto.ClientName + ".pdf");
        var Renderer = new ChromePdfRenderer(); // Instantiates Chrome Renderer
        var pdf = Renderer.RenderHtmlAsPdf(html);
        pdf.SaveAs(fullpath); // Saves our PdfDocument object as a PDF
        
    }


    public void PrintExcel(VenteDto venteDto)
    {
        using var workBook = new XLWorkbook();
        var workSheet = workBook.AddWorksheet("Invoices");
        
        workSheet.Cell("D1").Value = "Invoices Stock Manager";
            
        var range = workSheet.Range("D1:E1");
        range.Merge();
        range.Style.Font.Bold = true;
        range.Style.Font.FontSize = 22;
        range.Style.Font.FontColor = XLColor.White;
        range.Style.Fill.BackgroundColor = XLColor.Navy;

        workSheet.Row(1).Height = 30;
        // Table header
        
        workSheet.Cell("B3").Value = "Product Name";
        workSheet.Cell("C3").Value = "Quantity";
        workSheet.Cell("D3").Value = "Unit Price";
        workSheet.Cell("E3").Value = "Total Price";
        workSheet.Cell("F3").Value = "Customer Name";
        
        var range2 = workSheet.Range("B3:F3");

        workSheet.Columns("B:F").Width = 30;
        workSheet.Row(3).Height = 20;
        workSheet.Row(4).Height = 30;
        
        range2.Style.Font.Bold = true;
        range2.Style.Font.FontSize = 16;
        range2.Style.Font.FontColor = XLColor.White;
        range2.Style.Fill.BackgroundColor = XLColor.AirForceBlue;
        
        // Table body

        workSheet.Range("B4:F4").Style.Font.FontSize = 14;
        
        workSheet.Cell("B4").Value = venteDto.ProductName;
        workSheet.Cell("C4").Value = venteDto.Quantity;
        workSheet.Cell("D4").Value = venteDto.Price;
        workSheet.Cell("E4").Value = venteDto.Price * venteDto.Quantity;
        workSheet.Cell("F4").Value = venteDto.ClientName;
        workBook.SaveAs("/Users/abderahman/RiderProjects/StockManager/StockManager/Export/Excel/Invoices/"+RandomNumberGenerator.GetInt32(1, 1000)+"_"+venteDto.ProductName+"_"+venteDto.ClientName+".xlsx");
        
    }
}