using LiveCharts;
using LiveCharts.Definitions.Points;
using LiveCharts.Wpf;
using System;
using System.Windows.Media;

namespace Monefy.Models
{
    internal class MyChart 
    {
        public PieChart Chart { get; set; }
        public double Balance { get; set; }
        public DateTime Date { get; set; }

        public MyChart()
        { 
            Date = DateTime.Today;
            Chart = new();
        }
        
        public int searchIndex(Color color)
        {
            for (int i = 0; i < Chart.Series.Count; i++)
            {
                if (Chart.Series[i] is PieSeries pieSeries &&
                    pieSeries.Fill is SolidColorBrush seriesBrush &&
                    seriesBrush.Color == color)
                {
                    return i;
                }
            }
            return 0;
        }
    }
}