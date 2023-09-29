

class MenuManagement
{
    List<Account> Accounts { get; set; } = new();
    List<Quiz> Quizzes { get; set; } = new();
    public int CurrentAccount { get; private set; }

    public void Authorization()
    {
        int choice = 0;
        bool confirm = false;

        while (!confirm)
        {
            Console.WriteLine("1. Registration\n" +
                "2. Login");

            Quiz.CheckChoice(1, 2, ref choice);
            Console.Clear();

            string login, password;

            Console.WriteLine("Enter your login(Length: 2-10, You can use: A-Za-z0-9 and -_.): ");
            login = Console.ReadLine();

            Console.WriteLine("Enter your password(Length: 8-15, You can use: A-Za-z0-9 and -_.!)");
            password = Console.ReadLine();

            if (choice == 1)
            {
                DateTime birthDay;

                for (int i = 0; i < Accounts.Count; i++)
                {
                    if (Accounts[i].Login == login)
                    {
                        Console.WriteLine("This login is busy!");
                        confirm = true;
                        break;
                    }
                }

                if (!confirm)
                {
                    Console.WriteLine("Enter your birth day (yyyy-MM-dd):");
                    string userInput = Console.ReadLine();

                    if (DateTime.TryParse(userInput, out DateTime userBirthday))
                    {
                        confirm = true;
                        try
                        {
                            Accounts.Add(new Account(login, password, userBirthday));
                            CurrentAccount = Accounts.Count - 1;
                        }
                        catch (Exception e)
                        {
                            confirm = false;
                            Console.WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid birth day!");
                    }
                }
            }
            else
            {
                for (int i = 0; i < Accounts.Count; i++)
                {
                    if (Accounts[i].Login == login)
                    {
                        if (Accounts[i].Password == password)
                        {
                            CurrentAccount = i;
                            confirm = true;
                            break;
                        }
                        else
                            Console.WriteLine("Wrong password!");
                    }
                }

                if (!confirm)
                {
                    Console.WriteLine("Account not found!");
                }
            }
        }
    }

    public void CreateQuiz()
    {
        string category;

        Console.WriteLine("Enter quiz category: ");
        category = Console.ReadLine();

        Quizzes.Add(new Quiz(category));

        Quizzes[Quizzes.Count].CreateQuestions();
    }

    public void StartQuiz()
    {
        int choice = 0;

        Console.WriteLine("Choose the quiz you want to take: ");
        if (DisplayQuizzes())
        {
            Quiz.CheckChoice(1, Quizzes.Count, ref choice);
            Accounts[CurrentAccount].AddResult(Quizzes[choice].Category, Quizzes[choice].TakeTheQuiz(Accounts[CurrentAccount].Login));
        }
        else
            return;
    }

    public void DisplayAccountResult()
    {
        Accounts[CurrentAccount].DisplayMyResult();
    }

    public void DisplayTopInQuiz()
    {
        int choice = 0;

        Console.WriteLine("Choose the quiz: ");

        if (DisplayQuizzes())
        {
            Quiz.CheckChoice(1, Quizzes.Count, ref choice);
            Quizzes[choice].SortResult();
        }
        else
            return;
    }

    public void EditSetings()
    {
        int choice = 0;

        Console.WriteLine("1. Edit Password\n" +
            "2. Edit BirthDay");

        Quiz.CheckChoice(1, 2, ref choice);

        Console.WriteLine("Enter current password: ");
        string currentPassword = Console.ReadLine();

        if (choice == 1)
        {
            try
            {
                Accounts[CurrentAccount].EditPassword(currentPassword);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        else
        {
            try
            {
                Accounts[CurrentAccount].EditBirthDay(currentPassword);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }

    public bool DisplayQuizzes()
    {
        if (Quizzes.Count >= 1)
        {
            for (int i = 0; i < Quizzes.Count; i++)
            {
                Console.WriteLine($"Quiz {i + 1}: {Quizzes[i].Category}.");
            }
            return true;
        }
        else
        {
            Console.WriteLine("No quizzes, please add a quiz first");
            return false;
        }
    }
}
