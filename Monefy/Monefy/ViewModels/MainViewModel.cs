using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using Monefy.Services.Classes;

namespace Monefy.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly IMessenger Messenger;
        private string visibility = "Visible";
        private ViewModelBase currentView;

        public string Visibility
        {
            get => visibility;
            set 
            {
                Set(ref visibility, value);
            }
        }

        public ViewModelBase CurrentView
        {
            get => currentView;
            set
            {
                Set(ref currentView, value);
            }
        }

        public MainViewModel(IMessenger messenger)
        {
            Messenger = messenger;
            CurrentView = App.Container.GetInstance<ChartDataViewModel>();

            Messenger.Register<NavigationMessage>(this, message =>
            {
                CurrentView = message.ViewModelType;
                if (CurrentView == App.Container.GetInstance<OperationViewModel>())
                    Visibility = "Hidden";
                else
                    Visibility = "Visible";
            });
        }
    }
}
