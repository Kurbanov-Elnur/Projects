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
    private readonly ICardsManager _cardsManager;

    private string openMenuVisibility = "Visible";
    private string closeMenuVisibility = "Hidden";

    private double Amount;
    private string Description;

    public ObservableCollection<Card> Cards { get; set; }


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

    public CategoriesViewModel(IMessenger messenger, INavigationService navigationService, IDataService dataService, ICardsManager cardsManager)
    {
        _messenger = messenger;
        _navigationService = navigationService;
        _dataService = dataService;
        _cardsManager = cardsManager;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data != null && DateTime.TryParse(message.Data.ToString(), out DateTime result))
                CurrentDate = result;
            if (message.Data as ObservableCollection<Card> != null)
                Cards = message.Data as ObservableCollection<Card>;
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
                if(button as Button != null)
                {
                    Button Data = button as Button;

                    Transaction NewTransaction = new Transaction()
                    {
                        Date = CurrentDate,
                        Category = Data.Name,
                        Amount = Amount,
                        Description = this.Description,
                        Icon = new MyIcon((Data.Content as MaterialDesignThemes.Wpf.PackIcon).Kind.ToString(), Data.Foreground.ToString())
                    };
                    _dataService.SendData(NewTransaction);
                    _navigationService.NavigateTo<PayViewModel>();
                    return;
                }
                else
                {
                    Card card = button as Card;
                    _cardsManager.AddCardBalance(card.Number, Amount);
                    _navigationService.NavigateTo<ChartDataViewModel>();
                }
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

    public DelegateCommand<object> Select { get; private set; }
    public DelegateCommand OpenMenu { get; private set; }
    public DelegateCommand CloseMenu { get; private set; }
}