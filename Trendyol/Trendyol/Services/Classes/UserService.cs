using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trendyol.Models;
using Trendyol.Services.Interfaces;

using static BCrypt.Net.BCrypt;

namespace Trendyol.Services.Classes;

class UserService : IUserService
{
    private readonly AppContext _context;

    public UserService(AppContext context)
    {
        _context = context;
    }

    public void AddUser(User newUser)
    {
        newUser.Password = HashPassword(newUser.Password);

        _context.Users.Add(newUser);

        _context.SaveChanges();
    }

    public void RestorePassword(User user, string password)
    {
        user.Password = HashPassword(password);

        _context.SaveChanges();
    }
    
    public bool CheckData(string name, string surname, string password, string confirmPassword)
    {
        if (password != confirmPassword)
            throw new ArgumentException("Password mismatch!");

        if (!Regex.IsMatch(name, @"^[a-zA-Z]"))
            throw new ArgumentException("Wrong name!");

        if (!Regex.IsMatch(surname, @"^[a-zA-Z]"))
            throw new ArgumentException("Wrong surname!");

        if (!Regex.IsMatch(password, @"^[a-zA-Z0-9.]{8,}$"))
            throw new ArgumentException("Wrong password!");

        return true;
    }

    public bool CheckEmail(string email)
    {
        return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }
}