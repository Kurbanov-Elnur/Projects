MenuManagement Menu = new();

try
{
    Menu.Import();
}
catch (Exception e) { };

while (true)
{
    int choice = -1;

    Menu.Authorization();
    Console.Clear();

    while (choice != 6)
    {
        Console.Clear();

        Console.WriteLine("1. Start new quiz.\n" +
            "2. Create Quiz.\n" +
            "3. Display my results.\n" +
            "4. Display Top in Quizzes.\n" +
            "5. Edit settings.\n" +
            "6. Exit and safe.\n" +
            "(If you do not save and close the program, the changes will not be saved)");

        Quiz.CheckChoice(1, 6, ref choice);

        switch (choice)
        {
            case 1:
                Console.Clear();

                Menu.StartQuiz();
                Thread.Sleep(3000);

                break;
            case 2:
                Console.Clear();

                Menu.CreateQuiz();
                break;
            case 3:
                Console.Clear();

                Menu.DisplayAccountResult();
                Thread.Sleep(3000);

                break;
            case 4:
                Console.Clear();

                Menu.DisplayTopInQuiz();
                Thread.Sleep(3000);

                break;
            case 5:
                Console.Clear();

                Menu.EditSetings();
                Thread.Sleep(3000);

                break;
            case 6:
                Console.Clear();

                Menu.Export();
                return;
        }
    }
}