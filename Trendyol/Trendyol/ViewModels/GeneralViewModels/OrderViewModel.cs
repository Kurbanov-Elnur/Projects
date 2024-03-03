using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Models;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;

namespace Trendyol.ViewModels.GeneralViewModels;

class OrderViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly IOrderService _orderService;
    private string _orderStatus;

    public string Visibility { get; set; }
    public Order Order { get; set; }

    public string OrderStatus
    {
        get => _orderStatus;
        set
        {
            SetProperty(ref _orderStatus, value);
        }
    }

    public OrderViewModel(IMessenger messenger, IOrderService orderService)
    {
        _messenger = messenger;
        _orderService = orderService;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as Order != null)
            {
                Order = message.Data as Order;
                OrderStatus = Order.Status;
            }
            
            if(message.Data as User != null)
            {
                User user = message.Data as User;
                if (user.Role == "User")
                    Visibility = "Hidden";
                else
                    Visibility = "Visible";
            }
        });

        ChangeBackTheStatus = new(() =>
        {
            _orderService.ChangeBackTheStatus(Order);
        });

        ChangeTheStatusForward = new(() =>
        {
            _orderService.ChangeTheStatusForward(Order);
        });
    }

    public DelegateCommand ChangeBackTheStatus { get; set; }
    public DelegateCommand ChangeTheStatusForward { get; set; }
}