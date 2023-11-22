using LiveCharts;
using LiveCharts.Definitions.Points;
using LiveCharts.Wpf;
using MaterialDesignThemes.Wpf;
using System;
using System.Runtime.InteropServices;
using System.Windows.Media;

namespace Monefy.Models;

public class Transaction
{
    public double Amount { get; set; } 
    public string Description { get; set; }
    public MyIcon Icon { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; }
    public string PaymentСard { get; set; }
}