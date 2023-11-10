using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;

namespace Monefy.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentView;

        private readonly IMessenger Messenger;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                Set(ref _currentView, value);
            }
        }
        
        public MainViewModel(IMessenger messenger)
        {
            Messenger = messenger;
            CurrentView = App.Container.GetInstance<ChartDataViewModel>();

            Messenger.Register<NavigationMessage>(this, message =>
            {
                CurrentView = message.ViewModelType;
            });
        }
    }
}
