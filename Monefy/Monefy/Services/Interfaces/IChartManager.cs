using LiveCharts.Wpf;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monefy.Services.Interfaces
{
    interface IChartManager
    {
        public  double Count { get; set; }
        public void AddSerie(PieChart chart, Brush color);
    }
}