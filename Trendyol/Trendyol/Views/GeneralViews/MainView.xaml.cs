using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trendyol.Views.GeneralViews;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
    }

    private void Close(object sender, RoutedEventArgs e)
    {
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