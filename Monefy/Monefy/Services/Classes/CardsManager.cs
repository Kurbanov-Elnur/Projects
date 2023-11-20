using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Classes;

class CardsManager : ICardsManager
{
    private readonly IMessenger _messenger;

    public ObservableCollection<Card> Cards { get; set; } = new();

    public CardsManager(IMessenger messenger)
    {
        _messenger = messenger;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as ObservableCollection<Card> != null)
                Cards = message.Data as ObservableCollection<Card>;
        });
    }

    public void AddNewCard(Card newCard)
    {
        Cards.Add(newCard);
    }
}
