using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Messages;
using Trendyol.Data.Models;
using Trendyol.Services.Interfaces;
using static Trendyol.Services.Classes.DataService;

namespace Trendyol.Services.Classes;

class DataService : IDataService
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
    
    public void SendUser(User user)
    {
        _messenger.Send(new UserMessage()
        {
            User = user
        });
    }
}