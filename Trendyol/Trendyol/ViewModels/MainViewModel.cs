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
    private readonly IMessenger? _messenger;
    private readonly IDataService? _dataService;
    private readonly INavigationService _navigationService;

    private BindableBase? currentView;

    public BindableBase CurrentView
    {
        get => currentView ?? throw new NullReferenceException();
        set
        {
            SetProperty(ref currentView, value);
        }
    }

    public MainViewModel(IMessenger messenger, IDataService dataService, INavigationService navigationService)
    {
        _messenger = messenger;
        _dataService = dataService;
        _navigationService = navigationService;


        _messenger.Register<NavigationMessage>(this, message =>
        {
            CurrentView = message.ViewModelType;
        });

        Navigate = new(() =>
        {
        });
    }

    public DelegateCommand Navigate { get; private set; }
}