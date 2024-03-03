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
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;
using Trendyol.Messages;
using Trendyol.Services.Classes;
using Trendyol.Services.Interfaces;
using Trendyol.ViewModels.MenuViewModels;
using Trendyol.Views;

namespace Trendyol.ViewModels.GeneralViewModels;

class VerificateViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly IEmailVerificationService _emailVerificationService;
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;
    private readonly IUserService _userService;

    private MyAppContext _appContext;
    private string btnContent;
    private string sendCode;
    private User? User;

    public string BtnContent
    {
        get => btnContent;
        set
        {
            SetProperty(ref btnContent, value);
        }
    }

    public string Email { get; set; } = "";
    public string EnterredCode { get; set; } = "";

    public VerificateViewModel(IMessenger messenger, INavigationService navigationService, IEmailVerificationService emailVerificationService, IDataService dataService, IUserService userService, MyAppContext appContext)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _emailVerificationService = emailVerificationService;
        _dataService = dataService;
        _userService = userService;
        _appContext = appContext;

        BtnContent = "Send code";

        _messenger.Register<DataMessage>(this, message =>
        {
            if(message.Data as User != null)
                User = message.Data as User;
        });

        Verificate = new(() =>
        {
            if (_userService.CheckEmail(Email))
            {
                bool registeredEmail = _appContext.Users.Any(u => u.Email == Email);

                if(User != null && !registeredEmail)
                {
                    SwitchBtn();

                    if (EnterredCode == sendCode)
                    {
                        User.Email = Email;
                        userService.AddUser(User);

                        Back();
                    }
                }
                else if(User != null && registeredEmail)
                    MyMessageBoxWindow.Show("Email is registered", "Error", "Red");
                else if (User == null && registeredEmail)
                {
                    SwitchBtn();

                    if (EnterredCode == sendCode)
                    {
                        User = _appContext.Users.Single(u => u.Email == Email);

                        _dataService.SendData(User);

                        _navigationService.NavigateTo<ForgotPasswordViewModel>();

                        _dataService.SendData(new DelegateCommand(() =>
                        {
                            App.Container.GetInstance<ForgotPasswordViewModel>().Back();
                        }));
                    }
                }
                else
                {
                    MyMessageBoxWindow.Show("This email is not registered!", "Error", "Red");
                    Back();
                    return;
                }
            }
            else
            {
                MyMessageBoxWindow.Show("Invalid email format", "Error", "Red");
            }

        });
    }

    public DelegateCommand Verificate { get; private set; }

    private void SwitchBtn()
    {
        if (BtnContent == "Send code")
        {
            sendCode = _emailVerificationService.EmailVerification(Email);
            BtnContent = "Verify";
        }
        else
            BtnContent = "Send code";
    }

    public void Back()
    {
        if (User == null)
            _navigationService.NavigateTo<RegistrationViewModel>();
        else
            _navigationService.NavigateTo<LoginViewModel>();

        _navigationService.NavigateToMenu<SignInUpMenuViewModel>();

        BtnContent = "Send code";
        Email = "";
        EnterredCode = "";
    }
}