using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trendyol.Services.Interfaces;

namespace Trendyol.ViewModels;

class LoginViewModel : BindableBase
{
    private readonly INavigationService _navigationServie;

    public string Email { get; set; }
    public string Password { get; set; }

    public LoginViewModel(INavigationService navigationService)
    {
        _navigationServie = navigationService;

        SignIn = new(() =>
        {
            _navigationServie.NavigateToMenu<MainMenuViewModel>();
        });
        
        ForgotPassword = new(() =>
        {
            _navigationServie.NavigateTo<VerificateViewModel>();
        });
    }

    public DelegateCommand SignIn { get; private set; }
    public DelegateCommand ForgotPassword { get; private set; }
}