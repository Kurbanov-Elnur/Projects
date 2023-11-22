using GalaSoft.MvvmLight;
using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Monefy.ViewModels;

class CardsViewModel : BindableBase
{
    private readonly IDataService _dataService;
    private readonly INavigationService _navigationService;

    private ObservableCollection<Card> _cards;

    public ObservableCollection<Card> Cards
    {
        get => _cards;
        set
        {
            SetProperty(ref _cards, value);
        }
    }

    public CardsViewModel(INavigationService navigationService, IDataService dataService, IDeserializeService deserializeService)
    {
        _dataService = dataService;
        _navigationService = navigationService;


        Cards = deserializeService.Deserialize<Card>("Cards.json");
        _dataService.SendData(Cards);

        if (Cards == null)
            Cards = new();

        Back = new(() =>
        {
            _navigationService.NavigateTo<ChartDataViewModel>();
        });

        AddCard = new(() =>
        {
            _navigationService.NavigateTo<AddCardViewModel>();
        });

    }

    public DelegateCommand AddCard { get; private set; }
    public DelegateCommand Back { get; private set; }
}