using System.Text.RegularExpressions;
namespace CheckRegex;

static class MyRegex
{
    public static readonly Regex LoginRegex = new Regex(@"^[a-zA-Z0-9_.-]{2,10}$");
    public static readonly Regex PasswordRegex = new Regex(@"^[A-Za-z0-9_.!-]{8,15}$");
    public static readonly Regex LineRegex = new Regex(@"^[A-Za-z0-9_.!\-? ]+$");

    public static bool СheckLogin(string login)
    {
        return LoginRegex.IsMatch(login);
    }

    public static bool СheckPassword(string password)
    {
        return PasswordRegex.IsMatch(password);
    }

    public static bool СheckLine(string line)
    {
        return LineRegex.IsMatch(line);
    }
}
