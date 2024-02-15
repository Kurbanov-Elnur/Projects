using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Models;

namespace Trendyol.Services.Interfaces;

interface IUserService
{
    public void AddUser(User newUser);
    public bool CheckData(string name, string surname, string password, string confirmPassword);
    public bool CheckEmail(string email);
    public void RestorePassword(string email, string password);
}
