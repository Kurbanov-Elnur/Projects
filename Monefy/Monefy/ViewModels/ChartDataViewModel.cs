using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monefy.ViewModels
{
    internal class ChartDataViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public IChartManager chartManager { get; set; } 
        public Button _button { get; set; }
        public List<MyChart> Charts { get; set; } = new();
        public MyChart _currentChart;
        public MyChart CurrentChart
        {
            get => _currentChart;
            set
            {
                Set(ref _currentChart, value);
            }
        }

        public ChartDataViewModel(IChartManager manager, INavigationService navigationService)
        {
            chartManager = manager;
            Charts.Add(new MyChart());

            CurrentChart = Charts[Charts.Count - 1];
            _navigationService = navigationService;
        }

        public ButtonCommand<Button> Add
        {
            get => new(button =>
            {
                _button = button;
                _navigationService.NavigateTo<OperationViewModel>();

            });
        }

        public ButtonCommand Left
        {
            get => new(
            () =>
            {
                CurrentChart = Charts[searchIndex(CurrentChart.Date) - 1];
            },
            () =>
            {
                return searchIndex(CurrentChart.Date) - 1 >= Charts.Count - 1;
            });
        }            

        public ButtonCommand Right
        {
            get => new(
            () =>
            {
                CurrentChart = Charts[searchIndex(CurrentChart.Date) + 1];
            },
            () =>
            {
                return searchIndex(CurrentChart.Date) + 1 <= Charts.Count - 1;
            });

        }


        public int searchIndex(DateTime Date)
        {
            for (int i = 0; i < Charts.Count; i++)
            {
                if (Charts[i].Date == Date)
                    return i;
            }
            return 0;
        }
    }
}