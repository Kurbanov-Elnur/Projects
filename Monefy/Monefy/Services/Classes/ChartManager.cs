using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Interfaces;
using System.Windows.Media;

namespace Monefy.Serrvices.Classes;

class ChartManager : IChartManager
{
    private readonly INavigationService navigation;
    public double Count;

    public ChartManager(IMessenger messenger, INavigationService navigate)
    {
        navigation = navigate;
        messenger.Register<DataMessage>(this, (message) =>
        {
            double.TryParse(message.Data.ToString(),  out Count);
        });
    }

    public void AddSerie(MyChart MyChart, Brush color)
    {
        PieChart chart = MyChart.Chart;

        foreach (PieSeries item in chart.Series)
        {
            if (item.Fill == color)
            {
                item.Values[0] = (double)(Count + (double)item.Values[0]);
                return;
            }
            if ((item.Fill as SolidColorBrush).Color == Colors.Gray)
            {
                item.Values = new ChartValues<double> { 0 };
            }
        }

        chart.Series.Add(new PieSeries()
        {
            Fill = color,
            Values = new ChartValues<double> { Count },
            DataLabels = true,
        });

        MyChart.Balance += Count;
    }
}