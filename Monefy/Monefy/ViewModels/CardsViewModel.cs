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

namespace Monefy.ViewModels;

class CardsViewModel : BindableBase
{
    private readonly IDataService _dataService;
    private readonly INavigationService _navigationService;

    public ObservableCollection<Card> Cards { get; set; } = new();

    public CardsViewModel(INavigationService navigationService, IDataService dataService)
    {
        _dataService = dataService;
        _navigationService = navigationService;

        Cards.Add(new Card()
        {
            Name = "Elnur",
            Surname = "Mamedov",
            Number = "1234 1233 1231 2312",
            CVV = "123",
            MonthOfExpiry = "01",
            YearOfExpiry = "26",
            Balance = 120
        });

        Cards.Add(new Card()
        {
            Name = "Javad",
            Surname = "Rahimli",
            Number = "1234 1233 1231 2312",
            CVV = "123",
            MonthOfExpiry = "01",
            YearOfExpiry = "26",
            Balance = 120
        });

        Cards.Add(new Card()
        {
            Name = "Kenan",
            Surname = "Mammedli",
            Number = "1234 1233 1231 2312",
            CVV = "123",
            MonthOfExpiry = "01",
            YearOfExpiry = "26",
            Balance = 120
        });

        Back = new(() =>
        {
            _navigationService.NavigateTo<ChartDataViewModel>();
        });

        AddCard = new(() =>
        {
            _navigationService.NavigateTo<AddCardViewModel>();
        });

        _dataService.SendData(Cards);
    }

    public DelegateCommand AddCard { get; private set; }
    public DelegateCommand Back { get; private set; }
}