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

class TransactionsManager : ITransactionsManager
{
    public ObservableCollection<Transaction> Transactions { get; set; }

    public TransactionsManager(IMessenger messenger)
    {
        messenger.Register<DataMessage>(this, (message) =>
        {
            if (message.Data as ObservableCollection<Transaction> != null)
                Transactions = message.Data as ObservableCollection<Transaction>;
        });
    }

    public void AddTransaction(Transaction transaction)
    {
        Transactions.Add(transaction);
    }
}