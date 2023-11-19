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

namespace Monefy.Views
{
    public partial class CardsView : UserControl
    {
        public CardsView()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ScrollViewer scrollViewer)
            {
                double deltaY = -e.Delta;

                if (e.Delta > 0)
                {
                    scrollViewer.LineRight();
                }
                else if (e.Delta < 0)
                {
                    scrollViewer.LineLeft();
                }

                e.Handled = true;
            }
        }
    }
}
