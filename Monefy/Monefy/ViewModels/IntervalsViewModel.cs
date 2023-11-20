using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

namespace Monefy.ViewModels;

class IntervalsViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly IDataService _dataService;

    private string openMenuVisibility = "Visible";
    private string closeMenuVisibility = "Hidden";

    public string OpenMenuVisibility
    {
        get => openMenuVisibility;
        set
        {
            SetProperty(ref openMenuVisibility, value);
        }
    }

    public string CloseMenuVisibility
    {
        get => closeMenuVisibility;
        set
        {
            SetProperty(ref closeMenuVisibility, value);
        }
    }

    public IntervalsViewModel(IMessenger messenger, IDataService dataService)
    {
        _messenger = messenger;
        _dataService = dataService;

        _messenger.Register<DataMessage>(this, (message) =>
        {
            if(message.Data as string == "Hidden" || message.Data as string == "Visible")
                OpenMenuVisibility = message.Data as string;
        });

        OpenMenu = new(() =>
        {
            OpenMenuVisibility = "Hidden";
            CloseMenuVisibility = "Visible";
            App.Container.GetInstance<MainViewModel>().Visibility = "Hidden";
        });

        CloseMenu = new(() =>
        {
            OpenMenuVisibility = "Visible";
            CloseMenuVisibility = "Hidden";
        });
    }

    public DelegateCommand DayView { get; private set; }
    public DelegateCommand MonthView { get; private set; }
    public DelegateCommand YearView { get; private set; }
    public DelegateCommand OpenMenu { get; private set; }
    public DelegateCommand CloseMenu { get; private set; }
}