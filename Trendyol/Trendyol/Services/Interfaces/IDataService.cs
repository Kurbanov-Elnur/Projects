using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Models;

namespace Trendyol.Services.Interfaces;

interface IDataService
{
    public void SendData(object data);
    public void SendData(object[] data);
    public void SendUser(User user);
}