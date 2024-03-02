using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;
using Trendyol.Services.Interfaces;
using Trendyol.ViewModels.MenuViewModels;
using Trendyol.Views;
using static BCrypt.Net.BCrypt;
namespace Trendyol.ViewModels.GeneralViewModels;

class LoginViewModel : BindableBase
{
    private readonly INavigationService _navigationServie;
    private readonly IDataService _dataService;
    private readonly MyAppContext _appContext;

    public string Email { get; set; } = "";
    public string Password { get; set; } = "";

    public LoginViewModel(INavigationService navigationService, IDataService dataService, MyAppContext appContext)
    {
        _navigationServie = navigationService;
        _dataService = dataService;
        _appContext = appContext;

        SignIn = new(() =>
        {
            if (Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$") && _appContext.Users.Any(u => u.Email == Email))
            {
                User currentUser = _appContext.Users.Single(u => u.Email == Email);

                if (Verify(Password, currentUser.Password))
                {
                    _navigationServie.NavigateToMenu<MainMenuViewModel>();
                    _navigationServie.NavigateTo<GoodsViewModel>();
                    _dataService.SendData(currentUser);
                }
                else
                    MyMessageBoxWindow.Show("Wrong password!", "Error", "Red");
            }
            else
                MyMessageBoxWindow.Show("Wrong email!", "Error", "Red");
        });

        ForgotPassword = new(() =>
        {
            _navigationServie.NavigateTo<VerificateViewModel>();
            _dataService.SendData("Hidden");
        });
    }

    public DelegateCommand SignIn { get; private set; }
    public DelegateCommand ForgotPassword { get; private set; }
}