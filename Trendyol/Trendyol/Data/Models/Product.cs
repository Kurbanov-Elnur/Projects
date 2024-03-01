using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trendyol.Data.Models;

class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public double Price { get; set; }
    public byte[] Image { get; set; }

    public ICollection<Warehouse> Warehouse { get; set; }
    public ICollection<Order> Orders { get; set; }
}