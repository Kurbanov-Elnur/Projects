using CheckRegex;

class Account
{
    public string Login { get; private set; }
    public string Password { get; private set; }  
    public DateTime BirthDay { get; private set; }
    public Dictionary<string, double> MyResults { get; private set; }

    public Account(string login, string password, DateTime birthDay)
    {
        if (MyRegex.СheckLogin(login))
            Login = login;
        else
            throw new ArgumentException("Wrong login!");

        if (MyRegex.СheckPassword(password))
            Password = password;
        else
            throw new ArgumentException("Wrong password!");

        if (CalculateAge(birthDay) > 12 || CalculateAge(birthDay) < 80)
            BirthDay = birthDay;
        else
            throw new ArgumentException("Your age is not suitable(");

        MyResults = new();
    }

    public void AddResult(string quizCategory, double result)
    {
        MyResults.Add(quizCategory, result);
    }

    public void DisplayMyResult()
    {
        foreach (var kvp in MyResults)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}%");
        }
    }

    public void EditPassword(string currentPassword)
    {
        if(currentPassword == Password)
        {
            Console.Write("Enter new password(You can use letters, numbers and symbols \"_-.!\"): ");
            string newPassword = Console.ReadLine();
            if (MyRegex.СheckPassword(newPassword))
                Password = newPassword;
            else
                throw new ArgumentException("Wrong password!");
        }
    }

    public void EditBirthDay(string password)
    {
        if (password == Password)
        {
            Console.Write("Enter birth day(year-month-day): ");
            string birthDay = Console.ReadLine();
            if (DateTime.TryParse(birthDay, out DateTime newBirthDay) || CalculateAge(newBirthDay) > 12 || CalculateAge(newBirthDay) < 80)
                BirthDay = newBirthDay;
            else
                throw new ArgumentException("Wrong password!");
        }
    }

    public int CalculateAge(DateTime birthDay)
    {
        DateTime currentDate = DateTime.Now;
        int age = currentDate.Year - birthDay.Year;

        if (birthDay > currentDate.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
