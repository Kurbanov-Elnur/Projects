using GalaSoft.MvvmLight.Messaging;
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
    private readonly IMessenger _messenger;
    private readonly MyAppContext _appContext;

    private ObservableCollection<Order> _orders;

    public OrderService(IMessenger messenger, MyAppContext appContext)
    {
        _messenger = messenger;
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

    public void RemoveOrder(Order order)
    {
        _orders.Remove(order);

        _appContext.Remove(order);
        _appContext.SaveChanges();
    }
}