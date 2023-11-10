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

namespace Monefy.ViewModels
{
    internal class OperationViewModel : ViewModelBase
    {
        private readonly INavigationService NavigationService;
        private readonly IDataService _data;

        private double Balance = new();
        private StringBuilder Expression = new();

        public string _expressionText;

        public string ExpressionText
        {
            get => _expressionText;
            set
            {
                Set(ref _expressionText, value);
            }
        }

        public OperationViewModel(INavigationService navigationService, IDataService data)
        {
            NavigationService = navigationService;
            _data = data;
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
            get => new(() =>
            {
                if (Expression.Length > 0)
                {
                    Check();

                    ExpressionText = new System.Data.DataTable().Compute(Expression.ToString(), null).ToString();
                    Expression.Clear();
                    Expression.Append(ExpressionText);
                }
                else
                {
                    MessageBox.Show("Expression empty");
                }
            });
        }

        public ButtonCommand ReturnBalance
        {
            get => new(
            () =>
            {
                Balance = double.Parse(new System.Data.DataTable().Compute(Expression.ToString(), null).ToString());
                _data.SendData(Balance);
                App.Container.GetInstance<ChartDataViewModel>().Charts[App.Container.GetInstance<ChartDataViewModel>().searchIndex(App.Container.GetInstance<ChartDataViewModel>().CurrentChart.Date)].Chart = App.Container.GetInstance<ChartDataViewModel>().chartManager.AddSerie(App.Container.GetInstance<ChartDataViewModel>().Charts[App.Container.GetInstance<ChartDataViewModel>().searchIndex(App.Container.GetInstance<ChartDataViewModel>().CurrentChart.Date)].Chart, App.Container.GetInstance<ChartDataViewModel>()._button.Foreground);
                NavigationService.NavigateTo<ChartDataViewModel>();
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
}