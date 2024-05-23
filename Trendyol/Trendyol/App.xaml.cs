using GalaSoft.MvvmLight.Messaging;
using SimpleInjector;
using System.Configuration;
using System.Data;
using System.Transactions;
using System.Windows;
using Trendyol.Data.Contexts;
using Trendyol.Services.Classes;
using Trendyol.Services.Interfaces;
using Trendyol.ViewModels.AdminViewModels;
using Trendyol.ViewModels.GeneralViewModels;
using Trendyol.ViewModels.MenuViewModels;
using Trendyol.Views.GeneralViews;

namespace Trendyol
{
    public partial class App : Application
    {
        public static Container Container { get; set; } = new();

        public void Register()
        {
            Container.RegisterSingleton<MyAppContext>();

            Container.RegisterSingleton<IMessenger, Messenger>();
            Container.RegisterSingleton<INavigationService, NavigationService>();
            Container.RegisterSingleton<IDataService, DataService>();
            Container.RegisterSingleton<IUserService, UserService>();
            Container.RegisterSingleton<IEmailVerificationService, EmailVerificationService>();
            Container.RegisterSingleton<IGoodsService, GoodsService>();
            Container.RegisterSingleton<IOrderService, OrderService>();

            Container.RegisterSingleton<MainViewModel>();
            Container.RegisterSingleton<LoginViewModel>();
            Container.RegisterSingleton<MainMenuViewModel>();
            Container.RegisterSingleton<SignInUpMenuViewModel>();
            Container.RegisterSingleton<RegistrationViewModel>();
            Container.RegisterSingleton<VerificateViewModel>();
            Container.RegisterSingleton<ForgotPasswordViewModel>();
            Container.RegisterSingleton<GoodsViewModel>();
            Container.RegisterSingleton<ProductViewModel>();
            Container.RegisterSingleton<OrdersViewModel>();
            Container.RegisterSingleton<BackMenuViewModel>();
            Container.RegisterSingleton<OrderViewModel>();
            Container.RegisterSingleton<UserViewModel>();

            Container.Register<UsersViewModel>();
            Container.Register<AddProductViewModel>();

            Container.Verify();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Register();

            MainView window = new();

            window.DataContext = Container.GetInstance<MainViewModel>();

            window.ShowDialog();
        }
    }
}