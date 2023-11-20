using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Models;

class Card
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Number { get; set; }
    public string MonthOfExpiry { get; set; }
    public string YearOfExpiry { get; set; }
    public string CVV { get; set; }
    public double Balance { get; set; } 

    public Card(Card card)
    {
        Name = card.Name;
        Surname = card.Surname;
        Number = card.Number;
        MonthOfExpiry = card.MonthOfExpiry;
        YearOfExpiry = card.YearOfExpiry;
        CVV = card.CVV;
        Balance = card.Balance;
    }

    public Card()
    {
        Name = "";
        Surname = "";
        Number = "";
        MonthOfExpiry = "";
        YearOfExpiry = "";
        CVV = "";
        Balance = 0;
    }
}