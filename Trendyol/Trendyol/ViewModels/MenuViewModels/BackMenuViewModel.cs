using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;
using Trendyol.ViewModels.GeneralViewModels;

namespace Trendyol.ViewModels.MenuViewModels;

class BackMenuViewModel : BindableBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;

    public BackMenuViewModel(IMessenger messenger, INavigationService navigationService)
    {
        _messenger = messenger;
        _navigationService = navigationService;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as DelegateCommand != null)
                Back = message.Data as DelegateCommand;
        });
    }

    public DelegateCommand Back { get; set; }
}