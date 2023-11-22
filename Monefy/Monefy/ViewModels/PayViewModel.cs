using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Monefy.ViewModels;

class PayViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly ITransactionsManager _transactionsManager;
    private readonly INavigationService _navigationService;
    private readonly ICardsManager _cardsManager;

    public ObservableCollection<Card> Cards { get; set; }
    public Transaction Transaction { get; set; }

    public PayViewModel(IMessenger messenger, ITransactionsManager transactionsManager, INavigationService navigationService, ICardsManager cardsManager)
    {
        _messenger = messenger;
        _transactionsManager = transactionsManager;
        _navigationService = navigationService;
        _cardsManager = cardsManager;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as ObservableCollection<Card> != null)
                Cards = message.Data as ObservableCollection<Card>;
            if (message.Data as Transaction != null)
                Transaction = message.Data as Transaction;
        });

        Back = new(() =>
        {
            _navigationService.NavigateTo<ChartDataViewModel>();
        });

        Select = new ((selected) =>
        {
            Transaction.PaymentСard = selected.Number;
            _transactionsManager.AddTransaction(Transaction);
            _cardsManager.RemoveCardBalance(selected.Number, Transaction.Amount);
            _navigationService.NavigateTo<ChartDataViewModel>();
        });
    }

    public DelegateCommand<Card> Select { get; private set; }
    public DelegateCommand Back { get; private set; }
}