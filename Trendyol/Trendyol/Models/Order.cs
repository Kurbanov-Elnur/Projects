using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trendyol.Models;

class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string UserID { get; set; }
    public User User { get; set; }
    public string ProductID { get; set; }
    public Product Product { get; set; }
    public DateTime PurchaseDate { get; set;} = DateTime.Now;
}