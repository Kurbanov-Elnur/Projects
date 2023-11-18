using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Monefy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Monefy.Views;

public partial class MainView : Window
{
    private readonly SerializeService _serializeService;
    public MainView()
    {
        InitializeComponent();
        _serializeService = new();
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        _serializeService.Serialize("Data.json", App.Container.GetInstance<ChartDataViewModel>().Transactions);
        App.Current.Shutdown();
    }

    private void DragWindow(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
}