using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;

namespace Trendyol.ViewModels.GeneralViewModels;

class UsersViewModel : BindableBase
{
    private readonly MyAppContext _appContext;

    public ObservableCollection<User> Users { get; set; }

    public UsersViewModel(MyAppContext appContext)
    {
        _appContext = appContext;

        Users = new ObservableCollection<User> (_appContext.Users);
    }
}
