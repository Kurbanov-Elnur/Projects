using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using Monefy.Messages;
using Monefy.Models;
using Monefy.Serrvices.Classes;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Monefy.ViewModels;

class CategoriesViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;
    private readonly ITransactionsManager _transactionsManager;

    private string openMenuVisibility = "Visible";
    private string closeMenuVisibility = "Hidden";

    private double Amount;
    private DateTime CurrentDate { get; set; } 
    
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

    public CategoriesViewModel(IMessenger messenger, INavigationService navigationService, ITransactionsManager transactionsManager)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _transactionsManager = transactionsManager;

        _messenger.Register<DataMessage>(this, message =>
        {
            double.TryParse(message.Data.ToString(), out Amount);
            if (DateTime.TryParse(message.Data.ToString(), out DateTime result))
                CurrentDate = result;
        });

        Select = new((button) =>
        {
            if (Amount > 0)
            {
                Transaction NewTransaction = new Transaction()
                {
                    Date = CurrentDate,
                    Category = button.Name,
                    Amount = Amount,
                    Icon = new MyIcon((button.Content as PackIcon).Kind.ToString(), button.Foreground.ToString())
                };

                _transactionsManager.AddTransaction(NewTransaction);
            }
            _navigationService.NavigateTo<ChartDataViewModel>();
        });

        OpenMenu = new DelegateCommand(() =>
        {
            OpenMenuVisibility = "Hidden";
            CloseMenuVisibility = "Visible";
            App.Container.GetInstance<OperationViewModel>().SendData();
        });

        CloseMenu = new(() =>
        {
            OpenMenuVisibility = "Visible";
            CloseMenuVisibility = "Hidden";
        });
    }

    public DelegateCommand<Button> Select { get; private set; }
    public DelegateCommand OpenMenu { get; private set; }
    public DelegateCommand CloseMenu { get; private set; }
}