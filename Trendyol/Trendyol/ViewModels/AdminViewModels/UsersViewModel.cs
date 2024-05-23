using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;
using Trendyol.ViewModels.GeneralViewModels;
using Trendyol.ViewModels.MenuViewModels;

namespace Trendyol.ViewModels.AdminViewModels;

class UsersViewModel : BindableBase
{
    private readonly MyAppContext _appContext;
    private readonly IDataService _dataService;
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;

    public ObservableCollection<User> Users { get; set; }

    public UsersViewModel(MyAppContext appContext, IDataService dataService, IMessenger messenger, INavigationService navigationService)
    {
        _appContext = appContext;
        _dataService = dataService;
        _navigationService = navigationService;
        _messenger = messenger;

        Users = new ObservableCollection<User>(_appContext.Users);

        MoreInfo = new((user) =>
        {
            _dataService.SendUser(user);

            _navigationService.NavigateTo<UserViewModel>();
            _navigationService.NavigateToMenu<BackMenuViewModel>();

            _dataService.SendData(new DelegateCommand(() =>
            {
                _navigationService.NavigateTo<UsersViewModel>();
                _navigationService.NavigateToMenu<MainMenuViewModel>();

                if (App.Container.GetInstance<UserViewModel>().currentUser.Role == "Super Admin")
                    App.Container.GetInstance<UserViewModel>().Visibility = "Visible";
            }));
        });
    }

    public DelegateCommand<User> MoreInfo { get; set; }
}
