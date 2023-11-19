using GalaSoft.MvvmLight;
using Monefy.Services.Classes;
using Monefy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monefy.ViewModels;

internal class AddCardViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public AddCardViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public ButtonCommand Back
    {
        get => new(() =>
        {
            _navigationService.NavigateTo<CardsViewModel>();
        });
    }
}