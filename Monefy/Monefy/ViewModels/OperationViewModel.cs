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
using Prism.Commands;
using Prism.Mvvm;

namespace Monefy.ViewModels;

internal class OperationViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly IDataService _dataService;

    private double Amount = 0; 
    private StringBuilder Expression = new();
    public string _expressionText = "";

    public BindableBase ChoiceCategories { get; set; } = App.Container.GetInstance<CategoriesViewModel>();

    public string ExpressionText
    {
        get => _expressionText;
        set
        {
            SetProperty(ref _expressionText, value);
        }
    }

    public OperationViewModel(INavigationService navigationService, IDataService dataService)
    {
        _navigationService = navigationService;
        _dataService = dataService;

        Click = new((operation) =>
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

        Delete = new(() =>
        {
            if (Expression.Length > 0)
                Expression.Remove(Expression.Length - 1, 1);

            if (!string.IsNullOrEmpty(ExpressionText))
                ExpressionText = ExpressionText.Substring(0, ExpressionText.Length - 1);
        });

        Equal = new(
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

        Back = new(() =>
        {
            _navigationService.NavigateTo<ChartDataViewModel>();
            Expression.Clear();
            ExpressionText = "";
        });
    }

    public DelegateCommand<string> Click { get; private set; }
    public DelegateCommand Delete { get; private set; }
    public DelegateCommand Equal { get; private set; }
    public DelegateCommand Back { get; private set; }

    public bool SendData()
    {
        Check();

        if(Expression.Length > 0) 
            Amount = double.Parse(new System.Data.DataTable().Compute(Expression.ToString(), null).ToString());

        if (Amount == 0 || Amount < 0)
            return false;
        else
        {
            _dataService.SendData(Amount);
            return true;
        }
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