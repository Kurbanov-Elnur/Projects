using GalaSoft.MvvmLight;
using Monefy.Models;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModels
{
    class CardsViewModel : ViewModelBase
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
                Name = "MyCard",
                Number = "2345678",
                Balance = 1000000,
            });
            Cards.Add(new Card()
            {
                Name = "MyCard",
                Number = "12345678",
                Balance = 1000000,
            });
            Cards.Add(new Card()
            {
                Name = "MyCard",
                Number = "12345678",
                Balance = 1000000,
            });

            _dataService.SendData(Cards[0]);
        }

        public ButtonCommand AddCard
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<AddCardViewModel>();
            });
        }

        public ButtonCommand Back
        {
            get => new(() =>
            {
                _navigationService.NavigateTo<ChartDataViewModel>();
            });
        }
    }
}