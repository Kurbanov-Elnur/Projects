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
using Trendyol.ViewModels.GeneralViewModels;

namespace Trendyol.ViewModels.MenuViewModels;

class SignInUpMenuViewModel : BindableBase
{
    private readonly INavigationService _navigationServie;

    public SignInUpMenuViewModel(INavigationService navigationService)
    {
        _navigationServie = navigationService;

        ToSignIn = new(() =>
        {
            _navigationServie.NavigateTo<LoginViewModel>();
        });

        ToSignUp = new(() =>
        {
            _navigationServie.NavigateTo<RegistrationViewModel>();
        });
    }

    public DelegateCommand ToSignIn { get; private set; }
    public DelegateCommand ToSignUp { get; private set; }
}