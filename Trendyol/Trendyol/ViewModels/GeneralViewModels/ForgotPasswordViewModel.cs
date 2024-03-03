using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using Trendyol.Data.Models;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;
using Trendyol.ViewModels.MenuViewModels;
using Trendyol.Views;
using Trendyol.Views.MenuViews;

namespace Trendyol.ViewModels.GeneralViewModels;

class ForgotPasswordViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;
    private readonly IUserService _userService;

    private User User;
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public ForgotPasswordViewModel(IMessenger messenger, INavigationService navigationService, IDataService dataService, IUserService userService)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _dataService = dataService;
        _userService = userService;

        _messenger.Register<DataMessage>(this, message =>
        {
            if(message.Data as User != null)
                User = message.Data as User;
        });

        Confirm = new(() =>
        {
            if (Password != ConfirmPassword || !Regex.IsMatch(Password, @"^[a-zA-Z0-9.]{8,}$"))
                MyMessageBoxWindow.Show("Password mismatch!", "Error", "Red");
            else
            {
                _userService.RestorePassword(User, Password);

                Back();
            }
        });
    }

    public DelegateCommand Confirm { get; set; }

    public void Back()
    {
        _navigationService.NavigateTo<LoginViewModel>();
        _navigationService.NavigateToMenu<SignInUpMenuViewModel>();

        Password = "";
        ConfirmPassword = "";
        User = null;
    }
}