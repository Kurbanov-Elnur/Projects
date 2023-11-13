using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;

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
        get => intervalsView = App.Container.GetInstance<IntervalsViewModel>();
        set
        {
            Set(ref intervalsView, value);
        }
    }

    public ViewModelBase MoreInfoView
    {
        get => moreInfoView = App.Container.GetInstance<MoreInfoViewModel>();
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