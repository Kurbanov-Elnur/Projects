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
using Trendyol.ViewModels.AdminViewModels;
using Trendyol.ViewModels.MenuViewModels;

namespace Trendyol.ViewModels.GeneralViewModels;

class MainViewModel : BindableBase
{
    private readonly IMessenger _messenger;

    private BindableBase currentView;
    private BindableBase currentMenu;
    private string menuVisibility = "Visible";

    public BindableBase CurrentView
    {
        get => currentView;
        set
        {
            SetProperty(ref currentView, value);
        }
    }

    public BindableBase CurrentMenu
    {
        get => currentMenu;
        set
        {
            SetProperty(ref currentMenu, value);
        }
    }

    public string MenuVisibility
    {
        get => menuVisibility;
        set
        {
            SetProperty(ref menuVisibility, value);
        }
    }

    public MainViewModel(IMessenger messenger)
    {
        _messenger = messenger;

        CurrentView = App.Container.GetInstance<GoodsViewModel>();
        CurrentMenu = App.Container.GetInstance<SignInUpMenuViewModel>();

        _messenger.Register<NavigationMessage>(this, message =>
        {
            if (message.ViewModelType != null)
                CurrentView = message.ViewModelType;

            if (message.MenuModeltype != null)
                CurrentMenu = message.MenuModeltype;
        });

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data.ToString() == "Hidden" || message.Data.ToString() == "Visible")
                MenuVisibility = message.Data.ToString();
        });
    }
}