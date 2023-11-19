using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

public partial class AddCardView : UserControl
{
    public AddCardView()
    {
        InitializeComponent();
    }

    private void CheckNameSurname(object sender, TextCompositionEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (!char.IsLetter(e.Text, 0) || textBox.Text.Length >= 12)
        {
            e.Handled = true;
        }
    }

    private void CheckNumber(object sender, TextCompositionEventArgs e)
    {
        TextBox textBox = (TextBox)sender;

        if (!char.IsDigit(e.Text, 0) || textBox.Text.Length >= 19)
        {
            e.Handled = true;
        }

        if (!string.IsNullOrEmpty(textBox.Text))
        {
            string cleanedText = textBox.Text.Replace(" ", "");

            if (cleanedText.Length <= 16)
            {
                string formattedText = string.Join(" ", Regex.Matches(cleanedText, ".{1,4}").Cast<Match>());

                if (formattedText != textBox.Text)
                {
                    textBox.Text = formattedText;
                    textBox.CaretIndex = formattedText.Length;
                }
            }
        }
    }

    private void CheckMonth(object sender, TextCompositionEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (!char.IsDigit(e.Text, 0) || textBox.Text.Length >= 2)
        {
            e.Handled = true;
        }
    }

    private void CheckMonth(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;

        if (int.TryParse(textBox.Text, out int month))
        {
            if (month > 12)
            {
                textBox.Text = "";
            }
        }
    }

    private void CheckYear(object sender, TextCompositionEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (!char.IsDigit(e.Text, 0) || textBox.Text.Length >= 2)
        {
            e.Handled = true;
        }
    }

    private void CheckYear(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;

        if (textBox.Text.Length == 2)
        {
            if (int.TryParse(textBox.Text, out int month))
            {
                int currentYear = DateTime.Now.Year;

                if (month + 2000 < currentYear || month + 2000 > currentYear + 10)
                {
                    textBox.Text = "";
                }
            }
        }
    }

    private void CheckCVV(object sender, TextCompositionEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (!char.IsDigit(e.Text, 0) || textBox.Text.Length >= 3)
        {
            e.Handled = true;
        }
    }
}