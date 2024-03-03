using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;
using Trendyol.Messages;
using Trendyol.Services.Interfaces;
using Trendyol.Views;

namespace Trendyol.Services.Classes;

class GoodsService : IGoodsService
{
    private readonly IMessenger _messenger;
    private readonly MyAppContext _appContext;

    private ObservableCollection<Product> _products;
    private Warehouse _warehouse;

    public GoodsService(IMessenger messenger, MyAppContext appContext)
    {
        _messenger = messenger;
        _appContext = appContext;

        _messenger.Register<DataMessage>(this, message =>
        {
            if (message.Data as ObservableCollection<Product> != null)
                _products = message.Data as ObservableCollection<Product>;
        });
    }

    public void AddProduct(Product newProduct, int productCount)
    {
        try
        {
            CheckData(newProduct, productCount);
        }catch(Exception e)
        {
            MyMessageBoxWindow.Show(e.Message, "Error", "Red");
            return;
        }

        _warehouse = new()
        {
            ProductID = newProduct.Id,
            Product = newProduct,
            Count = productCount
        };

        _products.Add(newProduct);

        _appContext.Products.Add(newProduct);
        _appContext.Warehouse.Add(_warehouse);
        _appContext.SaveChanges();

        MyMessageBoxWindow.Show($"{newProduct.Name} successfully added", "SuccessCircleOutline", "Green");
    }

    public void RemoveProduct(Product product)
    {
        _products.Remove(product);

        _appContext.Products.Remove(product);
        _appContext.SaveChanges();
    }

    public void CheckData(Product product, int productCount)
    {
        if (string.IsNullOrEmpty(product.Name))
            throw new ArgumentException("Wrong name!");
        
        if (string.IsNullOrEmpty(product.Description))
            throw new ArgumentException("Wrong description!");
        
        if (string.IsNullOrEmpty(product.Brand))
            throw new ArgumentException("Wrong brand!");
        
        if (product.Price <= 0 && product.Price > 10000000)
            throw new ArgumentException("Wrong price!");
        
        if (productCount <= 0 && productCount > 100000)
            throw new ArgumentException("Wrong product count!");
        
        if (product.Image == null)
            throw new ArgumentException("Wrong image!");
    }
}