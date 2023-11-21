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

namespace Monefy.ViewModels;

class PayViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly ITransactionsManager _transactionsManager;
    private readonly INavigationService _navigationService;

    public ObservableCollection<Card> Cards { get; set; }
    public Transaction Transaction { get; set; }

    public PayViewModel(IMessenger messenger, ITransactionsManager transactionsManager, INavigationService navigationService)
    {
        _messenger = messenger;
        _transactionsManager = transactionsManager;
        _navigationService = navigationService;

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
    }

    public DelegateCommand Back { get; private set; }
}