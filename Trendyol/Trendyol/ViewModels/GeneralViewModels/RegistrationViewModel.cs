using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Trendyol.Data.Models;
using Trendyol.Services.Interfaces;
using Trendyol.Views;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Trendyol.ViewModels.GeneralViewModels;

class RegistrationViewModel : BindableBase
{
    private readonly INavigationService _navigationService;
    private readonly IUserService _userService;
    private readonly IDataService _dataService;
    private byte[] image;
    
    public User NewUser { get; set; }

    public string ConfirmPassword { get; set; }

    public byte[] Image
    {
        get => image;
        set
        {
            SetProperty(ref image, value);
        }
    }

    public RegistrationViewModel(INavigationService navigationService, IUserService userService, IDataService dataService)
    {
        _navigationService = navigationService;
        _userService = userService;
        _dataService = dataService;

        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string imagePath = Path.Combine(currentDirectory, "Data", "Models", "Profile.jpg");

        Image = File.ReadAllBytes(imagePath);

        NewUser = new();

        Forward = new(() =>
        {
            try
            {
                if (_userService.CheckData(NewUser.Name, NewUser.Surname, NewUser.Password, ConfirmPassword))
                {
                    if(Image != null)
                        NewUser.Image = Image;

                    _dataService.SendData(NewUser);

                    _navigationService.NavigateTo<VerificateViewModel>();
                }
            }
            catch (Exception e)
            {
                MyMessageBoxWindow.Show(e.Message, "Error", "Red");
            }
        });

        AddImage = new(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files | *.jpg; *.jpeg; *.png; *.gif; *.tif; ...";
            if (openFileDialog.ShowDialog() == true)
            {
                Image = File.ReadAllBytes(openFileDialog.FileName);
            }
        });
    }

    public DelegateCommand Forward { get; set; }
    public DelegateCommand AddImage { get; set; }
}