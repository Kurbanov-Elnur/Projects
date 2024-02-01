using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;

namespace Trendyol.ViewModels;

class LoginViewModel : BindableBase
{
    private readonly INavigationService _navigationService;

    public LoginViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        LoginCommand = new(
        () =>
        {
            _navigationService.NavigateTo<WelcomeViewModel>();
        });
    }

    public DelegateCommand LoginCommand { get; private set; }
}