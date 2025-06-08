using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StockManager.Dto;
using StockManager.Models;

namespace StockManager.ViewModels;

public partial class HistoryViewModel : ViewModelBase
{
    private readonly HistoryService _historyService;
    
    [ObservableProperty]
    private ObservableCollection<VenteDto> _venteDtos;

    [ObservableProperty]
    private string _msgFile = String.Empty;
    
    [ObservableProperty]
    private string _query = String.Empty;
    public HistoryViewModel()
    {
        _venteDtos = new ObservableCollection<VenteDto>();
        _historyService = new HistoryService();
        
        if (Query == "") ChargerVente();
        
    }
    [RelayCommand]
    private void ChargerVente()
    {
        VenteDtos.Clear();
        Query = "";
        Thread.Sleep(500);
        VenteDtos = _historyService.LoadSales();
    }
    
    [RelayCommand]
    private void ExportToExcel()
    {
        _historyService.ExportExcel();
        MsgFile = "Fichier Excel Exporté";
    }
    
    [RelayCommand]
    private void SearchSale(string query)
    {
        if (query == "") return;
        
            VenteDtos.Clear();
            Thread.Sleep(500);
            VenteDtos = _historyService.ChercherVente(query);  
    }
}