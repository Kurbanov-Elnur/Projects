using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;

namespace Trendyol.Services.Interfaces;

interface IGoodsService
{
    public void AddProduct(Product newProduct, int productCount);
    public void RemoveProduct(Product product);
}