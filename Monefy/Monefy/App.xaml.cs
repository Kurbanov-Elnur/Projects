using Monefy.ViewModels;
using System;
using System.Collections.Generic;
using SimpleInjector;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Monefy.Views;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Services.Interfaces;
using Monefy.Services.Classes;
using Monefy.Serrvices.Classes;
using System.Threading;

namespace Monefy;

public partial class App : Application
{
    public static Container Container  { get; set; } = new();

    public void Register()
    {
        Container.RegisterSingleton<IMessenger, Messenger>();
        Container.RegisterSingleton<INavigationService, NavigationService>();
        Container.RegisterSingleton<IDataService, DataService>();

        Container.RegisterSingleton<ITransactionsManager, TransactionsManager>();
        Container.RegisterSingleton<ICardsManager, CardsManager>();
        Container.RegisterSingleton<IChartManager, ChartManager>();
        Container.RegisterSingleton<IIntervalsManager, IntervalsManager>();
        Container.RegisterSingleton<ISerializeService, SerializeService>();
        Container.RegisterSingleton<IServerService, ServerService>();
        Container.RegisterSingleton<IDeserializeService, DeserializeService>();

        Container.RegisterSingleton<MainViewModel>();
        Container.RegisterSingleton<TransactionsViewModel>();
        Container.RegisterSingleton<CategoriesViewModel>();
        Container.RegisterSingleton<PayViewModel>();
        Container.RegisterSingleton<ChartDataViewModel>();
        Container.RegisterSingleton<OperationViewModel>();
        Container.RegisterSingleton<IntervalsViewModel>();
        Container.RegisterSingleton<CardsViewModel>();
        Container.RegisterSingleton<AddCardViewModel>();

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