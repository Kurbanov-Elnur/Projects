using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Monefy.Views;
using System.Threading;

namespace Monefy.ViewModels;

class MainViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly IDataService _dataService;
    private string visibility = "Visible";
    private ViewModelBase intervalsView;
    private ViewModelBase currentView;

    public string Visibility
    {
        get => visibility;
        set
        {
            Set(ref visibility, value);
        }
    }

    public ViewModelBase CurrentView
    {
        get => currentView;
        set
        {
            Set(ref currentView, value);
        }
    }

    public ViewModelBase IntervalsView
    {
        get => intervalsView;
        set
        {
            Set(ref intervalsView, value);
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