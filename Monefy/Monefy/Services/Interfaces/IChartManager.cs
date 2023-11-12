using LiveCharts.Wpf;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monefy.Services.Interfaces
{
    interface IChartManager
    {
        public  double Count { get; set; }
        public PieChart AddSerie(PieChart chart, Brush color);
    }
}