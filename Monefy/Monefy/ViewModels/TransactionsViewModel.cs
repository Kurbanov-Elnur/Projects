using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Serrvices.Classes;
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
using System.Windows;

namespace Monefy.ViewModels;

class TransactionsViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;

    private ObservableCollection<Transaction> transactions;
    private double totalExpenses;

    public ObservableCollection<Transaction> Transactions
    {
        get => transactions;
        set
        {
            SetProperty(ref transactions, value);
        }
    }

    public double TotalExpenses
    {
        get => totalExpenses;
        set
        {
            SetProperty(ref  totalExpenses, value);
        }
    }

    public TransactionsViewModel(INavigationService navigationService, IDataService dataService, IDeserializeService deserializeService)
    {
        _navigationService = navigationService;
        _dataService = dataService;

        Transactions = deserializeService.Deserialize<Transaction>("Data.json");

        if (Transactions == null)
            Transactions = new();

        ExpensesCalculation();

        _dataService.SendData(Transactions);

        Back = new(() =>
        {
            _navigationService.NavigateTo<ChartDataViewModel>();
        });

        MoreInfo = new((transaction) =>
        {
            _dataService.SendData(transaction);
            _navigationService.NavigateTo<TransactionMoreInfoViewModel>();
        });

        Transactions.CollectionChanged += (sender, e) =>
        {
            ExpensesCalculation();
        };
    }

    public DelegateCommand Back { get; private set; }
    public DelegateCommand<Transaction> MoreInfo { get; private set; }

    public void ExpensesCalculation()
    {
        TotalExpenses = 0;
        foreach (var item in Transactions)
        {
            TotalExpenses += item.Amount;
        }
    }
}