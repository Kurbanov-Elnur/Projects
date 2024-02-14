using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Messages;

namespace Trendyol.Services.Classes;

class NavigationService : Trendyol.Services.Interfaces.INavigationService
{
    private readonly IMessenger _messenger;
    public NavigationService(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public void NavigateTo<T>() where T : BindableBase
    {
        _messenger.Send(new NavigationMessage()
        {
            ViewModelType = App.Container.GetInstance<T>()
        });
    }

    public void NavigateToMenu<T>() where T : BindableBase
    {
        _messenger.Send(new NavigationMessage()
        {
            MenuModeltype = App.Container.GetInstance<T>()
        });
    }
}