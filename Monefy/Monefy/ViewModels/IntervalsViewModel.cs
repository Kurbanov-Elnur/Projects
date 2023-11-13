using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Monefy.ViewModels;

class IntervalsViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
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

    public IntervalsViewModel(IMessenger messenger)
    {
        _messenger = messenger;

        _messenger.Register<DatasMessage>(this, (message) =>
        {
            OpenMenuVisibility = message.Datas[0] as string;
            CloseMenuVisibility = message.Datas[1] as string;
        });
    }

    public ButtonCommand OpenMenu
    {
        get => new(() =>
        {
            OpenMenuVisibility = "Hidden";
            CloseMenuVisibility = "Visible";
        });
    }

    public ButtonCommand CloseMenu
    {
        get => new(() =>
        {
            OpenMenuVisibility = "Visible";
            CloseMenuVisibility = "Hidden";
        });
    }
}