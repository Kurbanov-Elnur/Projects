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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monefy.Views;

/// <summary>
/// Interaction logic for MoreInfoView.xaml
/// </summary>
public partial class MoreInfoView : UserControl
{
    private readonly IDataService _dataService;

    public MoreInfoView(IDataService dataService)
    {
        InitializeComponent();
        _dataService = dataService;
    }

    private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
    {
        _dataService.SendData(new object[] { "Visible", "Hidden" });
    }
}
