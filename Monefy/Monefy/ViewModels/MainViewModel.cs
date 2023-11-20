using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Monefy.Views;
using Prism.Mvvm;
using System.Threading;

namespace Monefy.ViewModels;

class MainViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly IDataService _dataService;
    private string visibility = "Visible";
    private BindableBase intervalsView;
    private BindableBase currentView;

    public string Visibility
    {
        get => visibility;
        set
        {
            SetProperty(ref visibility, value);
        }
    }

    public BindableBase CurrentView
    {
        get => currentView;
        set
        {
            SetProperty(ref currentView, value);
        }
    }

    public BindableBase IntervalsView
    {
        get => intervalsView;
        set
        {
            SetProperty(ref intervalsView, value);
        }
    }

    public MainViewModel(IMessenger messenger, IDataService dataService)
    {
        _messenger = messenger;
        _dataService = dataService;
        App.Container.GetInstance<TransactionsViewModel>();
        CurrentView = App.Container.GetInstance<ChartDataViewModel>();
        IntervalsView = App.Container.GetInstance<IntervalsViewModel>();

        _messenger.Register<NavigationMessage>(this, message =>
        {
            CurrentView = message.ViewModelType;
            if (CurrentView != App.Container.GetInstance<ChartDataViewModel>())
                Visibility = "Hidden";
            else
                Visibility = "Visible";

            _dataService.SendData(Visibility);
        });
    }
}