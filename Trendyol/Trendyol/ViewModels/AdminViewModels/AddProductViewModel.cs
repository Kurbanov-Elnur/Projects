using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;
using Trendyol.Views;

namespace Trendyol.ViewModels.AdminViewModels;

class AddProductViewModel : BindableBase
{
    private readonly MyAppContext _appContext;
    private byte[] image;
    private MyMessageBoxWindow myMessageBoxWindow;

    public Product NewProduct { get; set; }
    public Warehouse Warehouse { get; set; }

    public byte[] Image
    {
        get => image;
        set
        {
            SetProperty(ref image, value);
        }
    }

    public AddProductViewModel(MyAppContext appContext)
    {
        _appContext = appContext;
        NewProduct = new();
        Warehouse = new();

        AddProduct = new(() =>
        {
            myMessageBoxWindow = new(NewProduct.Name, "SuccessCircleOutline", "Green");
            NewProduct.Image = Image;

            Warehouse.ProductID = NewProduct.Id;
            Warehouse.Product = NewProduct;

            _appContext.Products.Add(NewProduct);
            _appContext.Warehouse.Add(Warehouse);
            _appContext.SaveChanges();
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