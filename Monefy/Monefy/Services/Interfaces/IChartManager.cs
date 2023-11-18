using LiveCharts;
using LiveCharts.Wpf;
using Monefy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monefy.Services.Interfaces;

interface IChartManager
{
    public void AddTransaction(ObservableCollection<Transaction> transactions, Button button);
    public void UpdateData(ObservableCollection<Transaction> transactions, SeriesCollection Data, DateTime Date);
}