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

namespace Trendyol.Views
{
    public partial class MyMessageBoxWindow : Window
    {
        public string Message { get; set; }
        public string MyIcon { get; set; }
        public string MyColor { get; set; }

        public MyMessageBoxWindow(string message, string icon, string color)
        {
            InitializeComponent();
            DataContext = this;
            Message = message;
            MyIcon = icon;
            MyColor = color;

            this.Left = 700;
            this.Top = 300;

            this.ShowDialog();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public static void Show(string message, string icon, string color)
        {
            MyMessageBoxWindow window = new(message, icon, color);
        }
    }
}
