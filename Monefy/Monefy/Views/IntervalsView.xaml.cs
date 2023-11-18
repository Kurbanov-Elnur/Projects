using Monefy.Services.Interfaces;
using Monefy.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

public partial class IntervalsView : UserControl
{
    public IntervalsView()
    {
        InitializeComponent();
    }

    public void BorderClose(object sender, MouseButtonEventArgs e)
    {
        App.Container.GetInstance<IntervalsViewModel>().OpenMenuVisibility = "Visible";
        App.Container.GetInstance<IntervalsViewModel>().CloseMenuVisibility = "Hidden";
        App.Container.GetInstance<MainViewModel>().Visibility = "Visible";
    }
}
