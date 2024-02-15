using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trendyol.Messages;
using Trendyol.Models;
using Trendyol.Services.Classes;
using Trendyol.Services.Interfaces;

namespace Trendyol.ViewModels;

class VerificateViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly IEmailVerificationService _emailVerificationService;
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;
    private readonly IUserService _userService;

    private string btnContent;
    private string sendCode;
    private User User;

    public string BtnContent
    {
        get => btnContent;
        set
        {
            SetProperty(ref btnContent, value);
        }
    }

    public string Email { get; set; }
    public string EnterredCode { get; set; }

    public VerificateViewModel(IMessenger messenger, INavigationService navigationService, IEmailVerificationService emailVerificationService, IDataService dataService, IUserService userService)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _emailVerificationService = emailVerificationService;
        _dataService = dataService;
        _userService = userService;

        BtnContent = "Send code";

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as User != null)
                User = message.Data as User;
        });


        Verificate = new(() =>
        {

            if (_userService.CheckEmail(Email))
            {
                if (User != null)
                {
                    if (BtnContent == "Send code")
                    {
                        sendCode = _emailVerificationService.EmailVerification(Email);
                        BtnContent = "Verify";
                    }

                    else if (EnterredCode == sendCode)
                    {
                        User.Email = Email;
                        userService.AddUser(User);
                    }
                }
                else
                {
                    if (BtnContent == "Send code")
                    {
                        sendCode = _emailVerificationService.EmailVerification(Email);
                        BtnContent = "Verify";
                    }
                    else if (EnterredCode == sendCode)
                    {
                        _dataService.SendData(Email);

                        _navigationService.NavigateTo<ForgotPasswordViewModel>();
                    }
                }
            }
            else
                MessageBox.Show("Wrong email!");
        });
    }

    public DelegateCommand Verificate { get; private set; }
}