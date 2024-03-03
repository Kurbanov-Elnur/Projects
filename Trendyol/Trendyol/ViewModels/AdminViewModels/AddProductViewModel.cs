using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;
using Trendyol.Services.Interfaces;
using Trendyol.ViewModels.GeneralViewModels;
using Trendyol.Views;

namespace Trendyol.ViewModels.AdminViewModels;

class AddProductViewModel : BindableBase
{
    private readonly IGoodsService _goodsService;
    private readonly INavigationService _navigationService;

    private byte[] image;

    public Product NewProduct { get; set; }
    public int ProductCount { get; set; } = 0;

    public byte[] Image
    {
        get => image;
        set
        {
            SetProperty(ref image, value);
        }
    }

    public AddProductViewModel(IGoodsService goodsService, INavigationService navigationService)
    {
        NewProduct = new();
        _goodsService = goodsService;
        _navigationService = navigationService;

        AddProduct = new(() =>
        {
            NewProduct.Image = Image;
            _goodsService.AddProduct(NewProduct, ProductCount);

            _navigationService.NavigateTo<GoodsViewModel>();
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

    public DelegateCommand AddProduct { get; set; }
    public DelegateCommand AddImage { get; set; }
}