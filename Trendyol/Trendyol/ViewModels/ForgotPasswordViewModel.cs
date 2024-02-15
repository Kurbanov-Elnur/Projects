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
using Trendyol.Messages;
using Trendyol.Services.Interfaces;
using Trendyol.Views;

namespace Trendyol.ViewModels;

class ForgotPasswordViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;
    private readonly IUserService _userService;

    private string Email;
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public ForgotPasswordViewModel(IMessenger messenger, INavigationService navigationService, IUserService userService)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _userService = userService;

        _messenger.Register<DataMessage>(this, message =>
        {
            Email = message.Data.ToString();
        });

        Confirm = new(() =>
        {
            if (Password != ConfirmPassword || !Regex.IsMatch(Password, @"^[a-zA-Z0-9.]{8,}$"))
                MessageBox.Show("Password mismatch!");
            else
            {
                _userService.RestorePassword(Email, Password);
                _navigationService.NavigateTo<LoginViewModel>();
            }
        });
    }

    public DelegateCommand Confirm { get; set; }
}