using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Models;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;

namespace Trendyol.ViewModels.AdminViewModels;

class UserViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly IUserService _userService;
    private string _userRole;

    public string Visibility { get; set; }
    public Order Order { get; set; }

    public string UserRole
    {
        get => _userRole;
        set
        {
            SetProperty(ref _userRole, value);
        }
    }

    public User User { get; set; }

    public UserViewModel(IMessenger messenger, IUserService userService)
    {
        _messenger = messenger;
        _userService = userService;

        _messenger.Register<UserMessage>(this, message =>
        {
            if(message.User  != null)
            {
                User = message.User;
                UserRole = User.Role;
            }
        });
        
        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as User != null)
            {
                User currentUser = message.Data as User;

                if (currentUser.Role == "Super Admin")
                    Visibility = "Visible";
                else
                    Visibility = "Hidden";
            }
        });

        ChangeBackTheRole = new(() =>
        {
            _userService.ChangeBackTheRole(User);
        });

        ChangeTheRoleForward = new(() =>
        {
            _userService.ChangeTheRoleForward(User);
        });
    }

    public DelegateCommand ChangeBackTheRole { get; set; }
    public DelegateCommand ChangeTheRoleForward { get; set; }
}