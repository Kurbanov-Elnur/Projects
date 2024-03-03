using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;
using Trendyol.ViewModels.MenuViewModels;
using Trendyol.Views.MenuViews;

namespace Trendyol.ViewModels.GeneralViewModels;

class GoodsViewModel : BindableBase
{
    private readonly MyAppContext _appContext;
    private readonly IDataService _dataService;
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;

    public ObservableCollection<Product> Products { get; set; }

    public GoodsViewModel(MyAppContext appContext, IDataService dataService, IMessenger messenger, INavigationService navigationService)
    {
        _appContext = appContext;
        _dataService = dataService;
        _navigationService = navigationService;
        _messenger = messenger;

        _messenger.Register<DataMessage>(this, message =>
        {
            if(message.Data as User != null)
            {
                User currentUser = message.Data as User;
                
                if(currentUser.Role == "User")
                    Products = new ObservableCollection<Product>(_appContext.Products.Where(p => p.Warehouse.Any(w => w.Count > 0)));
                else
                    Products = new ObservableCollection<Product>(_appContext.Products);

                _dataService.SendData(Products);
            }
        });

        MoreInfo = new((product) =>
        {
            _dataService.SendData(product);
            _navigationService.NavigateTo<ProductViewModel>();
            _navigationService.NavigateToMenu<BackMenuViewModel>();

            _dataService.SendData(new DelegateCommand(() =>
            {
                _navigationService.NavigateTo<GoodsViewModel>();
                _navigationService.NavigateToMenu<MainMenuViewModel>();
            }));
        });
    }

    public DelegateCommand<Product> MoreInfo { get; set; }
}