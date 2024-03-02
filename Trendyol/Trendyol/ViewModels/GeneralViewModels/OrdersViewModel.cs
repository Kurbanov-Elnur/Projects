using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
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
using Trendyol.ViewModels.MenuViewModels;

namespace Trendyol.ViewModels.GeneralViewModels;

class OrdersViewModel : BindableBase
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
                    Orders = new ObservableCollection<Order>(_appContext.Orders.Where(o => o.UserID == _currentUser.Id).Include(p => p.Product));
                else
                    Orders = new ObservableCollection<Order>(_appContext.Orders.Include(p => p.Product));

                _dataService.SendData(Orders);
            }
        });

        MoreInfo = new((order) =>
        {
            _dataService.SendData(order);

            _navigationService.NavigateTo<OrderViewModel>();
            _navigationService.NavigateToMenu<BackMenuViewModel>();

            _dataService.SendData(new DelegateCommand(() =>
            {
                _navigationService.NavigateTo<OrdersViewModel>();
                _navigationService.NavigateToMenu<MainMenuViewModel>();
            }));
        });
    }

    public DelegateCommand<Order> MoreInfo { get; set; }
}