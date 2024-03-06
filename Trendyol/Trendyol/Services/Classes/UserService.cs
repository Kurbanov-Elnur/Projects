using Prism.Commands;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trendyol.Data.Contexts;
using Trendyol.Data.Models;
using Trendyol.Services.Interfaces;

using static BCrypt.Net.BCrypt;

namespace Trendyol.Services.Classes;

class UserService : IUserService
{
    private readonly MyAppContext _context;
    private readonly IDataService _dataService;

    public UserService(MyAppContext context, IDataService dataService)
    {
        _context = context;
        _dataService = dataService;
    }

    public void AddUser(User newUser)
    {
        newUser.Password = HashPassword(newUser.Password);

        _context.Users.Add(newUser);

        _context.SaveChanges();
    }

    public void DeleteUser(User User)
    {
        _context.Users.Remove(User);

        _context.SaveChanges();
    }

    public void RestorePassword(User user, string password)
    {
        user.Password = HashPassword(password);

        _context.SaveChanges();
    }
    
    public bool CheckData(string name, string surname, string password, string confirmPassword)
    {
        if (!Regex.IsMatch(name, @"^[a-zA-Z]"))
            throw new ArgumentException("Wrong name!");

        if (!Regex.IsMatch(surname, @"^[a-zA-Z]"))
            throw new ArgumentException("Wrong surname!");

        if (!Regex.IsMatch(password, @"^[a-zA-Z0-9.]{8,}$"))
            throw new ArgumentException("Wrong password!");

        if (password != confirmPassword)
            throw new ArgumentException("Password mismatch!");

        return true;
    }

    public void ChangeBackTheRole(User user)
    {
        if (user.Role == "Admin")
        {
            user.Role = "User";
            _dataService.SendUser(user);
            _context.SaveChanges();
        }

    }

    public void ChangeTheRoleForward(User user)
    {
        if (user.Role == "User")
        {
            user.Role = "Admin";
            _dataService.SendUser(user);
            _context.SaveChanges();
        }
    }

    public bool CheckEmail(string email)
    {
        return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }
}