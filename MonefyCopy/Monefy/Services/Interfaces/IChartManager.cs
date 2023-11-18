using LiveCharts.Wpf;
using Monefy.Models;
using System.Windows.Controls;
using System.Windows.Media;

namespace Monefy.Services.Interfaces;

interface IChartManager
{
    public void AddSerie(MyChart chart, Brush color);
}