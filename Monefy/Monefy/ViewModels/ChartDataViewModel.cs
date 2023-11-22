using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
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
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shell;

namespace Monefy.ViewModels;

internal class ChartDataViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;
    private readonly IChartManager _chartManager;

    public ObservableCollection<Transaction> Transactions { get; set; }

    public double currentBalance;

    public double CurrentBalance
    {
        get => currentBalance;
        set
        {
            SetProperty(ref currentBalance, value);
        }
    }

    public SeriesCollection data = new();

    public SeriesCollection Data
    {
        get => data;
        set
        {
            SetProperty(ref data, value);
        }
    }

    public DateTime currentDate = DateTime.Today;

    public DateTime CurrentDate
    {
        get => currentDate;
        set
        {
            SetProperty(ref currentDate, value);
            _chartManager.UpdateData(Transactions, CurrentDate);
        }
    }

    public ChartDataViewModel(INavigationService navigationService, IDataService dataService, IMessenger messenger, IChartManager chartManager)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _dataService = dataService;
        _chartManager = chartManager;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as ObservableCollection<Transaction> != null)
                Transactions = message.Data as ObservableCollection<Transaction>;
        });

        _messenger.Register<BalanceMessage>(this, message =>
        {
            CurrentBalance = message.Balance;
        });

        Transactions = App.Container.GetInstance<TransactionsViewModel>().Transactions;

        _dataService.SendData(Data);
        _dataService.SendData(CurrentDate);

        _chartManager.UpdateData(Transactions, CurrentDate);

        Transactions.CollectionChanged += (sender, e) =>
        {
            _chartManager.UpdateData(Transactions, CurrentDate);
        };

        AddTransaction = new(
        () =>
        {
            _dataService.SendData(CurrentDate);
            _navigationService.NavigateTo<OperationViewModel>();
        },
        () =>
        {
            return App.Container.GetInstance<CardsViewModel>().Cards.Count != 0;
        });

        PreviousDay = new(
        () =>
        {
            CurrentDate = CurrentDate.AddDays(-1);
        });

        NextDay = new(
        () =>
        {
            CurrentDate = CurrentDate.AddDays(+1);
        });
    }

    public DelegateCommand AddTransaction { get; private set; }
    public DelegateCommand PreviousDay { get; private set; }
    public DelegateCommand NextDay { get; private set; }
}