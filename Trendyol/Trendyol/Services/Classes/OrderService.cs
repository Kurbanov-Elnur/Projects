using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;

namespace Trendyol.Services.Classes;

class OrderService : IOrderService
{
    private readonly string[] _statuses = new string[5]
    {
        "Canceled",
        "The order is being processed",
        "Accepted",
        "Sent",
        "At the post office"
    };

    private readonly IMessenger _messenger;
    private readonly IDataService _dataService;
    private readonly MyAppContext _appContext;

    private ObservableCollection<Order> _orders;

    public OrderService(IMessenger messenger, IDataService dataService, MyAppContext appContext)
    {
        _messenger = messenger;
        _dataService = dataService;
        _appContext = appContext;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as ObservableCollection<Order> != null)
                _orders = message.Data as ObservableCollection<Order>;
        });
    }

    public void AddOrder(Product product, User currentUser, int productCount)
    {
        Order newOrder = new()
        {
            UserID = currentUser.Id,
            User = currentUser,
            ProductID = product.Id,
            Product = product,
            Count = productCount
        };

        Warehouse warehouse = _appContext.Warehouse.Single(w => w.ProductID == product.Id);
        warehouse.Count -= productCount;

        _orders.Add(newOrder);

        _appContext.Orders.Add(newOrder);
        _appContext.SaveChanges();
    }

    public void ChangeBackTheStatus(Order order)
    {
        for (int i = 0; i < _statuses.Length; i++)
        {
            if (order.Status == _statuses[i] && i > 0)
            {
                order.Status = _statuses[i - 1];
                _dataService.SendData(order);
                break;
            }
        }

        _appContext.SaveChanges();
    }

    public void ChangeTheStatusForward(Order order)
    {
        for (int i = 0; i < _statuses.Length; i++)
        {
            if (order.Status == _statuses[i] && i < 3)
            {
                order.Status = _statuses[i + 1];
                _dataService.SendData(order);
                break;
            }
        }

        _appContext.SaveChanges();
    }
}