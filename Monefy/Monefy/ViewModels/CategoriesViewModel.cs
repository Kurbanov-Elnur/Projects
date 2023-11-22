using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
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
    private readonly IDataService _dataService;

    private string openMenuVisibility = "Visible";
    private string closeMenuVisibility = "Hidden";

    private double Amount;
    private string Description;
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

    public CategoriesViewModel(IMessenger messenger, INavigationService navigationService, IDataService dataService)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _dataService = dataService;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data != null && DateTime.TryParse(message.Data.ToString(), out DateTime result))
                CurrentDate = result;
        });

        _messenger.Register<DatasMessage>(this, message =>
        {
            double.TryParse(message.Datas[0].ToString(), out Amount);
            Description = message.Datas[1] as string;
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
                    Description = this.Description,
                    Icon = new MyIcon((button.Content as MaterialDesignThemes.Wpf.PackIcon).Kind.ToString(), button.Foreground.ToString())
                };
                _dataService.SendData(NewTransaction);
                _navigationService.NavigateTo<PayViewModel>();
                return;
            }
            _navigationService.NavigateTo<ChartDataViewModel>();
        });

        OpenMenu = new DelegateCommand(() =>
        {
            if (App.Container.GetInstance<OperationViewModel>().SendData())
            {
                OpenMenuVisibility = "Hidden";
                CloseMenuVisibility = "Visible";
            }
            else
                _navigationService.NavigateTo<ChartDataViewModel>();
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