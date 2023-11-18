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

internal class ChartDataViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;

    public ObservableCollection<Transaction> Transactions = new();

    public Card currentCard;

    public Card CurrentCard
    {
        get => currentCard;
        set
        {
            Set(ref currentCard, value);
        }
    }

    public SeriesCollection data = new();

    public SeriesCollection Data
    {
        get => data;
        set
        {
            Set(ref data, value);
        }
    }

    public DateTime currentDate = DateTime.Today;

    public DateTime CurrentDate
    {
        get => currentDate;
        set
        {
            Set(ref currentDate, value);
        }
    }

    public ChartDataViewModel(INavigationService navigationService, IDataService dataService, IMessenger messenger, IDeserializeService deserializeService, IChartManager chartManager)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _dataService = dataService;

        Transactions = deserializeService.Deserialize<Transaction>("Data.json");
        chartManager.UpdateData(Transactions, Data, DateTime.Today);

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as Card != null)
                CurrentCard = message.Data as Card;
        });
    }

    public ButtonCommand Add
    {
        get => new(() =>
        {
            _dataService.SendData(Transactions);
            _dataService.SendData(new object[] { CurrentDate, Data });
            _navigationService.NavigateTo<OperationViewModel>();
        });
    }
}