using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;

namespace Monefy.ViewModels;

class TransactionMoreInfoViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;
    private readonly ITransactionsManager _transactionsManager;
    private readonly ICardsManager _cardsManager;

    private Transaction transaction;

    public Transaction Transaction
    {
        get => transaction;
        set
        {
            SetProperty(ref transaction, value);
        }
    }

    public TransactionMoreInfoViewModel(IMessenger messenger, INavigationService navigationService, ITransactionsManager transactionsManager, ICardsManager cardsManager)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _transactionsManager = transactionsManager;
        _cardsManager = cardsManager;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as Transaction != null)
                Transaction = message.Data as Transaction;
        });

        Back = new(() =>
        {
            _navigationService.NavigateTo<TransactionsViewModel>();
        });

        RemoveTransaction = new(() =>
        {
            _transactionsManager.RemoveTransaction(Transaction);
            _cardsManager.AddCardBalance(Transaction.PaymentСard, Transaction.Amount);
            _navigationService.NavigateTo<TransactionsViewModel>();
        });
    }

    public DelegateCommand Back { get; private set; }
    public DelegateCommand RemoveTransaction { get; private set; }
}