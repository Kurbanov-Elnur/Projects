using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trendyol.Data.Models;

class Warehouse
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductID { get; set; }
    public Product Product { get; set; }
    public int Count { get; set; }
}