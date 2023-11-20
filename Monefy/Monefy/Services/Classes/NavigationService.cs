using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using Monefy.Messages;
using Monefy.Services.Interfaces;
using Prism.Mvvm;

namespace Monefy.Services.Classes;

class NavigationService : INavigationService
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
}