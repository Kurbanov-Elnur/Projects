using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Services.Interfaces;

namespace Trendyol.ViewModels;

class LoginViewModel : BindableBase
{
    private readonly INavigationService _navigationServie;

    public LoginViewModel(INavigationService navigationService)
    {
        _navigationServie = navigationService;

        SignIn = new(() =>
        {
            _navigationServie.NavigateToMenu<MainMenuViewModel>();
        });
    }

    public DelegateCommand SignIn { get; private set; }
}