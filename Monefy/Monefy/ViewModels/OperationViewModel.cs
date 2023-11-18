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
    private readonly IDataService _dataService;

    private double Balance = 0; 
    private StringBuilder Expression = new();
    private string _expressionText = "";
    public ViewModelBase ChoiceCategories { get; set; } = App.Container.GetInstance<CategoriesViewModel>();

    public string ExpressionText
    {
        get => _expressionText;
        set
        {
            Set(ref _expressionText, value);
        }
    }

    public OperationViewModel(INavigationService navigationService, IDataService dataService)
    {
        _navigationService = navigationService;
        _dataService = dataService;
    }

    public ButtonCommand<string> Click
    {
        get => new((operation) =>
        {
            if (operation != "+" && operation != "-" && operation != "*" && operation != "/")
            {
                if (Expression.Length == 0 && operation == "0")
                    return;
                if (ExpressionText.Contains(operation) && operation == ".")
                    return;
                ExpressionText += operation;
            }
            else
            {
                if (Expression.Length == 0)
                    return;
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

    public bool SendData()
    {
        Check();

        if(Expression.Length > 0) 
            Balance = double.Parse(new System.Data.DataTable().Compute(Expression.ToString(), null).ToString());

        if (Balance == 0 || Balance < 0)
            return false;
        else
        {
            _dataService.SendData(Balance);
            return true;
        }
    }

    public ButtonCommand Back
    {
        get => new(() =>
        {
            _navigationService.NavigateTo<ChartDataViewModel>();
            Expression.Clear();
            ExpressionText = "";
        });
    }

    public void Check()
    {
        if (Expression.Length > 0)
        {
            if (Expression[Expression.Length - 1].ToString() == "-" || Expression[Expression.Length - 1].ToString() == "+"
            || Expression[Expression.Length - 1].ToString() == "*" || Expression[Expression.Length - 1].ToString() == "/")
            {
                while (Expression[Expression.Length - 1] < 48 || Expression[Expression.Length - 1] > 57)
                    Expression.Remove(Expression.Length - 1, 1);
            }
        }
    }
}