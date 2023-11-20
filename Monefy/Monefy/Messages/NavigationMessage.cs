using GalaSoft.MvvmLight;
using Prism.Mvvm;

namespace Monefy.Messages;

class NavigationMessage
{
    public BindableBase ViewModelType { get; set; }
}