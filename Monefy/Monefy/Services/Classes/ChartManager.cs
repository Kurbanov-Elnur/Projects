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
    public SeriesCollection Data { get; set; }

    public ChartManager(IMessenger messenger)
    {

        messenger.Register<DataMessage>(this, (message) =>
        {
            if (message.Data as SeriesCollection != null)
                Data = message.Data as SeriesCollection;
        });
    }
    
    public void UpdateData(ObservableCollection<Transaction> transactions, DateTime Date)
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