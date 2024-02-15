using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Trendyol.Models;
using Trendyol.Services.Interfaces;

namespace Trendyol.ViewModels;

class RegistrationViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly IUserService _userService;
    private readonly IDataService _dataService;

    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    public RegistrationViewModel(INavigationService navigationService, IUserService userService, IDataService dataService)
    {
        _navigationService = navigationService;
        _userService = userService;
        _dataService = dataService;

        SignUp = new(() =>
        {
            try
            {
                if(_userService.CheckData(Name, Surname, Password, ConfirmPassword))
                {
                    _dataService.SendData(new User()
                    {
                        Name = Name,
                        Surname = Surname,
                        Email = "",
                        Password = Password,
                    });

                    _navigationService.NavigateTo<VerificateViewModel>();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        });
    }

    public DelegateCommand SignUp { get; set; }
}