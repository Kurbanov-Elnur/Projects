using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trendyol.Services.Interfaces;

namespace Trendyol.ViewModels;

class WelcomeViewModel : BindableBase
{
    private readonly INavigationService _navigationService;

    public WelcomeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void Completed()
    {
        _navigationService.NavigateTo<LoginViewModel>();
    }
}