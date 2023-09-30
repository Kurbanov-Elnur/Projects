using CheckRegex;

class Quiz
{
    public string Category { get; set; }
    public List<MyQuestion> Questions { get; set; } = new();
    public Dictionary<string, double> Results { get; private set; } = new();

    public Quiz(string category)
    {
        Category = category;
        Questions = new();
        Results = new();
    }

    public void CreateQuestions()
    {
        int questionCount = 0;
        Console.WriteLine($"Enter questions count(max {20 - Questions.Count}): ");
        CheckChoice(1, 20 - Questions.Count, ref questionCount);

        for (int i = 0; i < questionCount; i++)
        {
            string question = "#";
            while (!MyRegex.СheckLine(question))
            {
                Console.Write($"Enter question {i + 1}: ");
                question = Console.ReadLine();
            }

            List<string> Answers = new();

            int answerCount = 0;
            Console.WriteLine("Enter answers count(max 5): ");
            CheckChoice(1, 5, ref answerCount);

            for (int j = 0; j < answerCount; j++)
            {
                string answers = "#";
                while (!MyRegex.СheckLine(answers))
                {
                    Console.Write($"Enter answer {j + 1}: ");
                    answers = Console.ReadLine();
                }
                Answers.Add(answers);
            }

            int answer = 0;
            Console.WriteLine("Enter the correct answer: ");
            CheckChoice(1, Answers.Count, ref answer);

            Questions.Add(new MyQuestion(question, Answers, answer));
        }
    }

    public void RemoveQuestion()
    {
        int removeIndex = 0;

        DisplayQuestions();
        Console.WriteLine("Enter the number of the question you want to delete: ");
        CheckChoice(1, Questions.Count, ref removeIndex);

        Questions.Remove(Questions[removeIndex - 1]);
    }

    public double TakeTheQuiz(string login)
    {
        int result = 0;
        for (int i = 0; i < Questions.Count; i++)
        {
            int answer = 0;
            Questions[i].DisplayQuestion();

            Console.WriteLine("Enter answer: ");
            CheckChoice(1, Questions[i].Answers.Count, ref answer);

            if (Questions[i].CheckAnswer(answer))
                result++;
        }
        double res = Math.Round((double)result / Questions.Count * 100, 2);

        Console.WriteLine($"Count of correct answers: {res}%");
       
        Results.Add(login, res);

        return res;
    }

    public void DisplayQuestions()
    {
        for (int i = 0; i < Questions.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Questions[i].Question}");
        }
    }

    public void SortResult()
    {
        var sortedByValue = Results.OrderByDescending(pair => pair.Value);

        foreach (var kvp in sortedByValue)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }

    public static void CheckChoice(int start, int end, ref int choice)
    {
        Int32.TryParse(Console.ReadLine(), out choice);
        while (choice < start || choice > end)
        {
            Console.Write("Wrong choice!\n" +
                "Please re-enter: ");
            Int32.TryParse(Console.ReadLine(), out choice);
        }
    }
}