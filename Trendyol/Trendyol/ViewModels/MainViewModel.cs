using GalaSoft.MvvmLight.Messaging;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;

namespace Trendyol.ViewModels;

class MainViewModel : BindableBase
{
    private readonly IMessenger? _messenger;
    private readonly IDataService? _dataService;

    private BindableBase? currentView;

    public BindableBase CurrentView
    {
        get => currentView ?? throw new NullReferenceException();
        set
        {
            SetProperty(ref currentView, value);
        }
    }

    public MainViewModel(IMessenger messenger, IDataService dataService)
    {
        _messenger = messenger;
        _dataService = dataService;

        CurrentView = App.Container.GetInstance<WelcomeViewModel>();

        _messenger.Register<NavigationMessage>(this, message =>
        {
            CurrentView = message.ViewModelType;
        });
    }
}