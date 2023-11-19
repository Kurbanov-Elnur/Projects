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
    public DateTime DateOfExpiry { get; set; }
    public short CVV { get; set; }
    public double Balance { get; set; }
}