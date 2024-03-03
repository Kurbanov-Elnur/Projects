using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trendyol.Data.Models;

class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TrackID { get; set; } = Guid.NewGuid().ToString();

    public string UserID { get; set; }
    public User User { get; set; }
    public string ProductID { get; set; }
    public Product Product { get; set; }
    public int Count { get; set; }
    public DateTime PurchaseDate { get; set; } = DateTime.Now;
    public string Status { get; set; } = "The order is being processed";
}