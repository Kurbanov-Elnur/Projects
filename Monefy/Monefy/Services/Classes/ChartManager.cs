using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using Monefy.Messages;
using Monefy.Services.Interfaces;
using Monefy.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monefy.Serrvices.Classes
{
    class ChartManager : IChartManager
    {
        private readonly INavigationService navigation;
        public static double Count { get; set; }

        public ChartManager(IMessenger messenger, INavigationService navigate)
        {
            navigation = navigate;
            messenger.Register<DataMessage>(this, (message) =>
            {
                Count = (double)message.Data;
            });
        }
         
        public PieChart AddSerie(PieChart chart, Brush color)
        {
            foreach (PieSeries item in chart.Series)
            {
                if (item is PieSeries pieSeries)
                    if (pieSeries.Fill == color)
                    {
                        pieSeries.Values = new ChartValues<double> { Count + (double)pieSeries.Values[0] };
                        return chart;
                    }
            }

            chart.Series.Add(new PieSeries()
            {
                Fill = color,
                Values = new ChartValues<double> { Count },
                DataLabels = true,
            });

            return chart;
        }
    }
}