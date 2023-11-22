﻿using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Services.Interfaces;

namespace Monefy.Serrvices.Classes;

public class DataService : IDataService
{
    private readonly IMessenger _messenger;

    public DataService(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void SendData(object data)
    {
        _messenger.Send(new DataMessage()
        {
            Data = data
        });
    }

    public void SendData(object[] data)
    {
        _messenger.Send(new DatasMessage()
        {
            Datas = data
        });
    }

    public void SendBalance(double balance)
    {
        _messenger.Send(new BalanceMessage()
        {
            Balance = balance
        });
    }
}