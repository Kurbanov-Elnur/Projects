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

namespace Trendyol.ViewModels.GeneralViewModels;

class GoodsViewModel : BindableBase
{
    private readonly MyAppContext _appContext;

    public ObservableCollection<Product> Products { get; set; }

    public GoodsViewModel(MyAppContext appContext)
    {
        _appContext = appContext;
        Products = new ObservableCollection<Product>(_appContext.Products);

        Command = new(() =>
        {
            MessageBox.Show("ads");
        });
    }

    public DelegateCommand Command { get; set; } 
}