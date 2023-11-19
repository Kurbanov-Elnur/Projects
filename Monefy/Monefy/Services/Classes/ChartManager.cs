using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Interfaces;
using Monefy.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Monefy.Serrvices.Classes;

class ChartManager : IChartManager
{
    private readonly INavigationService navigation;
    private readonly IDataService _dataService;

    public double Count;
    public DateTime CurrentDate;
    SeriesCollection Data;

    public ChartManager(IMessenger messenger, INavigationService navigate, IDataService dataService)
    {
        navigation = navigate;
        _dataService = dataService;

        messenger.Register<DataMessage>(this, (message) =>
        {
            double.TryParse(message.Data.ToString(), out Count);
        });

        messenger.Register<DatasMessage>(this, (message) =>
        {
            if (DateTime.TryParse(message.Datas[0].ToString(), out CurrentDate) && message.Datas[1] as SeriesCollection != null)
            {
                DateTime.TryParse(message.Datas[0].ToString(), out CurrentDate);
                Data = message.Datas[1] as SeriesCollection;
            }
        });
    }

    public void AddTransaction(ObservableCollection<Transaction> transactions, Button button)
    {
        transactions.Add(new Transaction()
        {
            Category = button.Name,
            Amount = Count,
            Icon = new MyIcon((button.Content as PackIcon).Kind.ToString(), button.Foreground.ToString())
        });

        _dataService.SendData(transactions);
    }
    
    public void UpdateData(ObservableCollection<Transaction> transactions, SeriesCollection Data, DateTime Date)
    {
        Data.Clear();

        for (int i = 0; i < transactions.Count; i++)
        {
            if (transactions[i].Date == Date)
            {
                bool contains = false;

                foreach (var item in Data)
                {
                    var series = item as ColumnSeries;
                    if (series.Fill.ToString() == transactions[i].Icon.Color.ToString())
                    {
                        series.Values = new ChartValues<double> { transactions[i].Amount + (double)series.Values[0] };
                        contains = true;
                        break;
                    }
                }

                if(!contains)
                {
                    Data.Add(new ColumnSeries()
                    {
                        Values = new ChartValues<double> { transactions[i].Amount },
                        Fill = transactions[i].Icon.Color,
                        Title = transactions[i].Category,
                        DataLabels = true
                    });
                }
            }
        }
    }
}