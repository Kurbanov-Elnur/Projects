using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Serrvices.Classes;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Monefy.ViewModels;

class CategoriesViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;
    private readonly IChartManager _chartManager;

    private string openMenuVisibility = "Visible";
    private string closeMenuVisibility = "Hidden";

    public ObservableCollection<Transaction> Transactions;

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

    public CategoriesViewModel(IMessenger messenger, INavigationService navigationService, IChartManager chartManager)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _chartManager = chartManager;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as ObservableCollection<Transaction> != null)
                Transactions = message.Data as ObservableCollection<Transaction>;
        });
    }

    public ButtonCommand<Button> Select
    {
        get => new((button) =>
        {
            _chartManager.AddTransaction(Transactions, button);
            _navigationService.NavigateTo<ChartDataViewModel>();
        });
    }

    public ButtonCommand OpenMenu
    {
        get => new(() =>
        {
            OpenMenuVisibility = "Hidden";
            CloseMenuVisibility = "Visible";
        },
        () =>
        {
            OpenMenuVisibility = "Hidden";
            CloseMenuVisibility = "Visible";
            return App.Container.GetInstance<OperationViewModel>().SendData();
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