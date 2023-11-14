using GalaSoft.MvvmLight.Command;
using System.Text;
using Monefy.Services.Interfaces;
using GalaSoft.MvvmLight;
using System.Windows;
using Monefy.Models;
using GalaSoft.MvvmLight.Messaging;
using Monefy.Messages;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using Monefy.Services.Classes;
using Monefy.Serrvices.Classes;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using LiveCharts.Wpf.Charts.Base;
using System.Drawing;
using System.Security.RightsManagement;
using MaterialDesignThemes.Wpf;

namespace Monefy.ViewModels;

internal class OperationViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IChartManager _chartManager;
    private readonly IDataService _dataService;

    private double Balance = new();
    private StringBuilder Expression = new();
    private string _expressionText;

    private MyChart chart;
    private Button MyButton;
    private PackIcon icon;

    public MyChart Chart
    {
        get => chart;
        set
        {
            Set(ref chart, value);
        }
    }

    public  PackIcon Icon
    {
        get => icon;
        set
        {
            Set(ref icon, value);
        }
    }

    public string ExpressionText
    {
        get => _expressionText;
        set
        {
            Set(ref _expressionText, value);
        }
    }

    public OperationViewModel(INavigationService navigationService, IChartManager chartManager, IMessenger messenger, IDataService dataService)
    {
        _navigationService = navigationService;
        _chartManager = chartManager;
        _dataService = dataService;

        messenger.Register<DatasMessage>(this, message =>
        {
            Chart = message.Datas[0] as MyChart;
            MyButton = message.Datas[1] as Button;
            Icon = MyButton.Content as PackIcon;
        });
    }

    public ButtonCommand<string> Click
    {
        get => new((operation) =>
        {
            if (operation != "+" && operation != "-" && operation != "*" && operation != "/")
                ExpressionText += operation;
            else
            {
                Check();
                ExpressionText = "";
            }
            Expression.Append(operation);
        });
    }

    public ButtonCommand Delete
    {
        get => new(() =>
        {
            if (Expression.Length > 0)
                Expression.Remove(Expression.Length - 1, 1);

            if (!string.IsNullOrEmpty(ExpressionText))
                ExpressionText = ExpressionText.Substring(0, ExpressionText.Length - 1);
        });
    }

    public ButtonCommand Equal
    {
        get => new(
        () =>
        {
            if (Expression.Length > 0)
            {
                Check();

                ExpressionText = new System.Data.DataTable().Compute(Expression.ToString(), null).ToString();
                Expression.Clear();
                Expression.Append(ExpressionText);
            }
        },
        () =>
        {
            return !(Expression.Length == 0);
        });
    }

    public ButtonCommand ReturnBalance
    {
        get => new(
        () =>
        {
            Check();

            Balance = double.Parse(new System.Data.DataTable().Compute(Expression.ToString(), null).ToString());

            _dataService.SendData(Balance);
            _chartManager.AddSerie(Chart, MyButton.Foreground);
            _navigationService.NavigateTo<ChartDataViewModel>();

            Expression.Clear();
            ExpressionText = "";
        },
        () =>
        {
            return !(Expression.Length == 0);
        });
    }

    public void Check()
    {
        if (Expression[Expression.Length - 1].ToString() == "-" || Expression[Expression.Length - 1].ToString() == "+"
        || Expression[Expression.Length - 1].ToString() == "*" || Expression[Expression.Length - 1].ToString() == "/")
        {
            while(Expression[Expression.Length - 1] < 48 || Expression[Expression.Length - 1] > 57)
                Expression.Remove(Expression.Length - 1, 1);
        }
    }
}