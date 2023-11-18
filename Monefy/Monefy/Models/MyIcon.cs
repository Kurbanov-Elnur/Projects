using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Monefy.Models;

public class MyIcon
{
    public string Name { get; set; }
    public Brush Color { get; set; }

    public MyIcon(string name, string color)
    {
        Name = name;
        Color = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));
    }
}
