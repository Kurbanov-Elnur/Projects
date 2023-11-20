using GalaSoft.MvvmLight;
using Prism.Mvvm;

namespace Monefy.Services.Interfaces;

public interface INavigationService
{
    public void NavigateTo<T>() where T : BindableBase;
}