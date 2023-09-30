using CheckRegex;

class MyQuestion
{
    public string Question { get; set; }
    public List<string> Answers { get; set; }
    public int Answer { get; set; }

    public MyQuestion(string question, List<string> answers, int answer)
    {
        Question = question;
        Answers = answers;
        Answer = answer;
    }

    public bool CheckAnswer(int answer)
    {
        if (Answer == answer)
            return true;
        else
            return false;
    }

    public void DisplayQuestion()
    {
        Console.WriteLine(Question);

        for (int i = 0; i < Answers.Count; i++)
        {
            Console.WriteLine($"Answer {i + 1}: {Answers[i]}");
        }
    }
}