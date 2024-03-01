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
using Trendyol.Data.Models;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;
using Trendyol.Views;

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
            User = message.Data as User;
        });

        Confirm = new(() =>
        {
            if (Password != ConfirmPassword || !Regex.IsMatch(Password, @"^[a-zA-Z0-9.]{8,}$"))
                MessageBox.Show("Password mismatch!");
            else
            {
                _userService.RestorePassword(User, Password);
                _navigationService.NavigateTo<LoginViewModel>();

                _dataService.SendData("Visible");
            }
        });

        Back = new(() =>
        {
            _navigationService.NavigateTo<LoginViewModel>();

            _dataService.SendData("Visible");
        });
    }

    public DelegateCommand Confirm { get; set; }
    public DelegateCommand Back { get; set; }
}