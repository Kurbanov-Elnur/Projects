using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trendyol.Data.Models;

namespace Trendyol.Services.Interfaces;

interface IUserService
{
    public void AddUser(User newUser);
    public void DeleteUser(User User);
    public bool CheckData(string name, string surname, string password, string confirmPassword);
    public bool CheckEmail(string email);
    public void ChangeBackTheRole(User user);
    public void ChangeTheRoleForward(User user);
    public void RestorePassword(User user, string password);
}
