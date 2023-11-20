using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModels;

class TransactionsViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;

    private ObservableCollection<Transaction> transactions;

    public ObservableCollection<Transaction> Transactions
    {
        get => transactions;
        set
        {
            SetProperty(ref transactions, value);
        }
    }

    public TransactionsViewModel(INavigationService navigationService, IDataService dataService, IDeserializeService deserializeService)
    {
        _navigationService = navigationService;
        _dataService = dataService;

        Transactions = deserializeService.Deserialize<Transaction>("Data.json");

        if (Transactions == null)
            Transactions = new();

        _dataService.SendData(Transactions);

        Back = new(() =>
        {
            _navigationService.NavigateTo<ChartDataViewModel>();
        });
    }

    public DelegateCommand Back { get; private set; }
}