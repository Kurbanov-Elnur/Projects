using Monefy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces;

interface ICardsManager
{
    public ObservableCollection<Card> Cards { get; set; }
    public void AddNewCard(Card newCard);
}