using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
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

namespace Trendyol.ViewModels.GeneralViewModels;

class OrdersViewModel
{
    private readonly MyAppContext _appContext;
    private readonly IDataService _dataService;
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;
    
    private User _currentUser { get; set; }

    public ObservableCollection<Order> Orders { get; set; }

    public OrdersViewModel(MyAppContext appContext, IDataService dataService, INavigationService navigationService, IMessenger messenger)
    {
        _appContext = appContext;
        _dataService = dataService;
        _navigationService = navigationService;
        _messenger = messenger;

        Orders = new();

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as User != null)
            {
                _currentUser = message.Data as User;

                if(_currentUser.Role == "User")
                {
                    Orders = new ObservableCollection<Order>(_appContext.Orders.Where(o => o.UserID == _currentUser.Id));
                }
                else
                    Orders = new ObservableCollection<Order>(_appContext.Orders);

                _dataService.SendData(Orders);
            }
        });

        MoreInfo = new((product) =>
        {
            _dataService.SendData(product);
            _navigationService.NavigateTo<ProductViewModel>();
        });
    }

    public DelegateCommand<Product> MoreInfo { get; set; }
}