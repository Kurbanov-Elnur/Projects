using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Monefy.Views;

namespace Monefy.ViewModels;

class MainViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly IDataService _dataService;
    private string visibility = "Visible";
    private ViewModelBase intervalsView;
    private ViewModelBase moreInfoView;
    private ViewModelBase currentView;

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

    public ViewModelBase MoreInfoView
    {
        get => moreInfoView;
        set
        {
            Set(ref moreInfoView, value);
        }
    }

    public MainViewModel(IMessenger messenger, IDataService dataService)
    {
        _messenger = messenger;
        _dataService = dataService;
        CurrentView = App.Container.GetInstance<ChartDataViewModel>();
        IntervalsView = App.Container.GetInstance<IntervalsViewModel>();
        MoreInfoView = App.Container.GetInstance<MoreInfoViewModel>();

        _messenger.Register<NavigationMessage>(this, message =>
        {
            CurrentView = message.ViewModelType;
            if (CurrentView == App.Container.GetInstance<OperationViewModel>())
                visibility = "Hidden";
            else
                visibility = "Visible";

            _dataService.SendData(visibility);
        });
    }
}