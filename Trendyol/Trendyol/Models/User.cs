using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trendyol.Models;

class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string email { get; set; }
    public string password { get; set; }
    public string OrderId { get; set; }
    public Order Order { get; set; }
}