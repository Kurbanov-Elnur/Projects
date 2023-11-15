using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System.Windows;

namespace Monefy.ViewModels;

class IntervalsViewModel : ViewModelBase
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
            Set(ref openMenuVisibility, value);
        }
    }

    public string CloseMenuVisibility
    {
        get => closeMenuVisibility;
        set
        {
            Set(ref closeMenuVisibility, value);
        }
    }

    public IntervalsViewModel(IMessenger messenger, IDataService dataService)
    {
        _messenger = messenger;
        _dataService = dataService;

        _messenger.Register<DataMessage>(this, (message) =>
        {
            OpenMenuVisibility = message.Data as string;
        });
    }

    public ButtonCommand DayView
    {
        get => new(() =>
        {
        });
    }  
    
    public ButtonCommand MonthView
    {
        get => new(() =>
        {
        });
    }  
    
    public ButtonCommand YearView
    {
        get => new(() =>
        {
        });
    }

    public ButtonCommand OpenMenu
    {
        get => new(() =>
        {
            OpenMenuVisibility = "Hidden";
            CloseMenuVisibility = "Visible";

            _dataService.SendData("Hidden");
        });
    }

    public ButtonCommand CloseMenu
    {
        get => new(() =>
        {
            OpenMenuVisibility = "Visible";
            CloseMenuVisibility = "Hidden";

            _dataService.SendData("Visible");
        });
    }
}