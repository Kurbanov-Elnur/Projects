using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;

namespace Trendyol.Services.Interfaces;

interface IOrderService
{
    public void AddOrder(Product product, User currentUser, int productCount);
    public void ChangeBackTheStatus(Order order);
    public void ChangeTheStatusForward(Order order);
}