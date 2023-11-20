using GalaSoft.MvvmLight;
using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Monefy.ViewModels;

internal class AddCardViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly ICardsManager _cardsManager;

    public Card newCard;

    public Card NewCard
    {
        get => newCard;
        set
        {
            SetProperty(ref newCard, value);
        }
    }

    public AddCardViewModel(INavigationService navigationService, ICardsManager cardsManager)
    {
        _navigationService = navigationService;
        _cardsManager = cardsManager;

        newCard = new Card();

        AddNewCard = new(() =>
        {
            _cardsManager.AddNewCard(new Card(NewCard));
            Clear();
            _navigationService.NavigateTo<CardsViewModel>();
        });

        Back = new(() =>
        {
            Clear();
            _navigationService.NavigateTo<CardsViewModel>();
        });
    }

    public DelegateCommand AddNewCard { get; private set; }
    public DelegateCommand Back { get; private set; }

    public void Clear()
    {
        newCard.Name = "";
        newCard.Surname = "";
        newCard.CVV = "";
        newCard.YearOfExpiry = "";
        newCard.MonthOfExpiry = "";
        newCard.Number = "";
    }
}