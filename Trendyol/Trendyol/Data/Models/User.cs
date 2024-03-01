using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trendyol.Data.Models;

class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
    public byte[] Image { get; set; }

    public ICollection<Order> Orders { get; set; }
}