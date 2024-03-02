using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;
using Trendyol.ViewModels.MenuViewModels;
using Trendyol.Views;

namespace Trendyol.ViewModels.GeneralViewModels;

class ProductViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly MyAppContext _myAppContext;
    private readonly INavigationService _navigationService;
    private readonly IGoodsService _goodsService;
    private readonly IOrderService _orderService;

    private User _currentUser;

    public string CountVisibility { get; set; }
    public string BtnContent { get; set; }
    public Product Product { get; set; }
    public int InStock { get; set; }
    public int ProductCount { get; set; }

    public ProductViewModel(IMessenger messenger, MyAppContext myAppContext, INavigationService navigationService, IGoodsService goodsService, IOrderService orderService)
    {
        _messenger = messenger;
        _myAppContext = myAppContext;
        _navigationService = navigationService;
        _goodsService = goodsService;
        _orderService = orderService;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as Product != null)
            {
                Product = message.Data as Product;
                InStock = _myAppContext.Warehouse.Single(p => p.ProductID == Product.Id).Count;
            }

            if (message.Data as User != null)
            {
                _currentUser = message.Data as User;

                if (_currentUser.Role == "User")
                {
                    BtnContent = "Buy";
                    CountVisibility = "Visible";
                }
                else
                {
                    BtnContent = "Delete";
                    CountVisibility = "Hidden";
                }
            }
        });

        BtnCommand = new(() =>
        {
            if (BtnContent == "Buy")
            {
                if (ProductCount > 0 && ProductCount <= InStock)
                    _orderService.AddOrder(Product, _currentUser, ProductCount);
                else
                {
                    MyMessageBoxWindow.Show("Invalid product count!", "Error", "Red");
                    return;
                }
            }
            else
                _goodsService.RemoveProduct(Product);

            ProductCount = 0;

            _navigationService.NavigateTo<GoodsViewModel>();

            if(_currentUser.Role == "User")
                _navigationService.NavigateToMenu<MainMenuViewModel>();
            else
                _navigationService.NavigateToMenu<MainMenuViewModel>();
        });
    }

    public DelegateCommand BtnCommand { get; set; }
}