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

        Products = new ObservableCollection<Product>(_appContext.Products);
        _dataService.SendData(Products);

        MoreInfo = new((product) =>
        {
            _dataService.SendData(product);
            _navigationService.NavigateTo<ProductViewModel>();
        });
    }

    public DelegateCommand<Product> MoreInfo { get; set; } 
}