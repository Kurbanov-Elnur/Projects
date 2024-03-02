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
using Trendyol.Services.Interfaces;
using Trendyol.ViewModels.MenuViewModels;
using Trendyol.Views.MenuViews;

namespace Trendyol.ViewModels.GeneralViewModels;

class GoodsViewModel : BindableBase
{
    private readonly MyAppContext _appContext;
    private readonly IDataService _dataService;
    private readonly INavigationService _navigationService;

    public ObservableCollection<Product> Products { get; set; }

    public GoodsViewModel(MyAppContext appContext, IDataService dataService, INavigationService navigationService)
    {
        _appContext = appContext;
        _dataService = dataService;
        _navigationService = navigationService;

        Products = new ObservableCollection<Product>(_appContext.Products.Where(p => p.Warehouse.Any(w => w.Count > 0)));
        _dataService.SendData(Products);

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