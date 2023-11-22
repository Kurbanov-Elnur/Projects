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
    private readonly IDataService _dataService;

    public ObservableCollection<Card> Cards { get; set; } = new();
    public double TotalBalance { get; set; }

    public CardsManager(IMessenger messenger, IDataService dataService)
    {
        _messenger = messenger;
        _dataService = dataService;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as ObservableCollection<Card> != null)
            {
                Cards = message.Data as ObservableCollection<Card>;
                BalanceCalculation();
            }
        });

        _dataService.SendBalance(TotalBalance);
    }

    public void AddNewCard(Card newCard)
    {
        foreach (var item in Cards)
        {
            if (item.Number == newCard.Number)
                return;
        }
        Cards.Add(newCard);
        BalanceCalculation();
    }

    public void RemoveCard(Card card)
    {
        Cards.Remove(card);
        BalanceCalculation();
    }

    public void AddCardBalance(string cardNumber, double amount)
    {
        foreach (var item in Cards)
        {
            if (item.Number == cardNumber)
            {
                item.Balance += amount;
                BalanceCalculation();
                return;
            }
        }
    }

    public void RemoveCardBalance(string cardNumber, double amount)
    {
        foreach (var item in Cards)
        {
            if (item.Number == cardNumber)
            {
                item.Balance -= amount;
                BalanceCalculation();
                return;
            }
        }
    }

    public void BalanceCalculation()
    {
        TotalBalance = 0;
        foreach (var item in Cards)
        {
            TotalBalance += item.Balance;
        }
        _dataService.SendBalance(TotalBalance);
    }
}