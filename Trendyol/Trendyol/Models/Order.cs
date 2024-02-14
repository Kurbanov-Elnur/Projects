using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trendyol.Models;

class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string name { get; set; }

    public ICollection<User> users { get; set; }
}