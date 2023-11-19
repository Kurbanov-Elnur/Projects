using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModels;

class TransactionsViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;

    private ObservableCollection<Transaction> transactions;

    public ObservableCollection<Transaction> Transactions
    {
        get => transactions;
        set
        {
            Set(ref transactions, value);
        }
    }

    public TransactionsViewModel(INavigationService navigationService, IMessenger messenger)
    {
        _navigationService = navigationService;
        _messenger = messenger;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as ObservableCollection<Transaction> != null)
                Transactions = message.Data as ObservableCollection<Transaction>;
        });
    }

    public ButtonCommand Back
    {
        get => new(() =>
        {
            _navigationService.NavigateTo<ChartDataViewModel>();
        });
    }
}