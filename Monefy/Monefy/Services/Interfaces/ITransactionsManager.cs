using Monefy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.Services.Interfaces;

interface ITransactionsManager
{
    public ObservableCollection<Transaction> Transactions { get; set; }
    public void AddTransaction(Transaction transaction);
}