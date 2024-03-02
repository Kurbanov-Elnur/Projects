using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trendyol.Services.Interfaces;

interface INavigationService
{
    public void NavigateTo<T>() where T : BindableBase;
    public void NavigateToMenu<T>() where T : BindableBase;
}