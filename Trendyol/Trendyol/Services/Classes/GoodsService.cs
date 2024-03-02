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
    }

    public void RemoveProduct(Product product)
    {
        _products.Remove(product);

        _appContext.Remove(product);
        _appContext.SaveChanges();
    }
}