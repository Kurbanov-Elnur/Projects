using Management;

MenuManagement Menu = new();

try
{
    Menu.ImportInProgram();
}
catch (Exception e){ };

while(true)
{
    int choice = -1;

    Menu.SelectCurrentDictionary();
    Console.Clear();

    while (choice != 6)
    {
        choice = -1;
        Menu.DisplayCurrentDictioany();

        Console.WriteLine("1. Add Word or Translation.\n" + 
		    "2. Replace Word or Translation.\n" + 
		    "3. Remove Word or Translation.\n" +
		    "4. Search Word.\n" +
            "5. Display dictionay data\n" +
            "6. Go back.\n" +
		    "7. Exit and Save");

        Menu.CheckChoice(1, 7, ref choice);

        switch (choice)
        {
            case 1:
                Console.Clear();

                Menu.AddWord();
                break;
            case 2:
                Console.Clear();

                Menu.ReplaceWord();
                break;
            case 3:
                Console.Clear();

                Menu.DeleteWord();
                break;
            case 4:
                Console.Clear();

                Menu.SearchWord();
                break;
            case 5:
                Console.Clear();

                Menu.DisplayDictionaryData();
                break;
            case 6:
                Console.Clear();
                break;
            case 7:
                Menu.ExportInCSV();
                Environment.Exit(0);
                break;
        }
    }
}