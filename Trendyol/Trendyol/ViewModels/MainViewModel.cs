using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;

namespace Trendyol.ViewModels;

class MainViewModel : BindableBase
{
    private readonly IMessenger _messenger;

    private BindableBase currentView;
    private BindableBase currentMenu;

    public BindableBase CurrentView
    {
        get => currentView ?? throw new NullReferenceException();
        set
        {
            SetProperty(ref currentView, value);
        }
    }
    
    public BindableBase CurrentMenu
    {
        get => currentMenu ?? throw new NullReferenceException();
        set
        {
            SetProperty(ref currentMenu, value);
        }
    }

    public MainViewModel(IMessenger messenger)
    {
        _messenger = messenger;

        CurrentView = App.Container.GetInstance<LoginViewModel>();
        CurrentMenu = App.Container.GetInstance<SignInUpMenuViewModel>();

        _messenger.Register<NavigationMessage>(this, message =>
        {
            if(message.ViewModelType != null)
                CurrentView = message.ViewModelType;

            if (message.MenuModeltype != null)
                CurrentMenu = message.MenuModeltype;
        });
    }
}