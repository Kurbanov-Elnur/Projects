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
using Trendyol.ViewModels.AdminViewModels;
using Trendyol.ViewModels.GeneralViewModels;

namespace Trendyol.ViewModels.MenuViewModels;

class MainMenuViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly IMessenger _messenger;

    private User _currentUser;
    public string AdminMenuVisibility { get; set; }

    public MainMenuViewModel(INavigationService navigationService, IMessenger messenger)
    {
        _navigationService = navigationService;
        _messenger = messenger;


        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as User != null)
            {
                _currentUser = message.Data as User;

                if (_currentUser.Role == "User")
                    AdminMenuVisibility = "Collapsed";
                else
                    AdminMenuVisibility = "Visible";
            }
        });

        GoToGoods = new(() =>
        {
            _navigationService.NavigateTo<GoodsViewModel>();
        }); 
        
        GoToOrders = new(() =>
        {
            _navigationService.NavigateTo<OrdersViewModel>();
        });
        
        GoToAddProduct = new(() =>
        {
            _navigationService.NavigateTo<AddProductViewModel>();
        });

        GoToProfile = new(() =>
        {
            _navigationService.NavigateTo<ProfileViewModel>();
        });
    }

    public DelegateCommand GoToGoods { get; set; }
    public DelegateCommand GoToOrders { get; set; }
    public DelegateCommand GoToAddProduct { get; set; }
    public DelegateCommand GoToProfile { get; set; }
}