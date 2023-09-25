namespace Management;

class MenuManagement
{
    private DictionariesManagement DictionariesManagement = new();
    public ushort CurrentDictionary { get; private set; }

    public void SelectCurrentDictionary()
    {
        int choice = -1;

        if (DictionariesManagement.Dictionaries.Count > 0)
        {
            Console.WriteLine("Select current dictionary: ");
            Console.Write(DictionariesManagement);
            Console.WriteLine("Enter 0 for create new dictionary.");

            while (choice < 0 || choice > DictionariesManagement.Dictionaries.Count)
            {
                Int32.TryParse(Console.ReadLine(), out choice);
            }

            if(choice == 0)
            {
                CreateDictionary();
                CurrentDictionary = 0;
                return; 
            }

            CurrentDictionary = (ushort)(choice -= 1);
        }
        else
        {
            CreateDictionary();
            CurrentDictionary = 0;
        }
    }

    public void CreateDictionary()
    {
        string[] dictionaryType = new string[2];

        List<string> languages = new List<string>()
                {
                    new string("English"),
                    new string("Russia"),
                    new string("German"),
                    new string("Georgia"),
                    new string("French"),
                    new string("Swedish"),
                    new string("Chine"),
                    new string("Korea"),
                };

        Console.WriteLine("Choose the language of the words: ");
        for (int i = 0; i < languages.Count; i++)
            Console.WriteLine($"{i + 1}. {languages[i]}");

        int choice = 0;
        while (choice < 1 || choice > languages.Count)
        {
            Console.Write("Enter your choice: ");
            Int32.TryParse(Console.ReadLine(), out choice);
        }

        dictionaryType[0] = languages[choice - 1];

        languages.Remove(languages[choice - 1]);
        choice = 0;

        Console.WriteLine("Select the translation language: ");
        for (int i = 0; i < languages.Count; i++)
            Console.WriteLine($"{i + 1}. {languages[i]}");

        while (choice < 1 || choice > languages.Count)
        {
            Console.Write("Enter your choice: ");
            Int32.TryParse(Console.ReadLine(), out choice);
        }

        dictionaryType[1] = languages[choice - 1];

        DictionariesManagement.AddNewDictionary(dictionaryType);
    }

    public void InsertWord()
    {
        string Word, Translation;

        Console.WriteLine("Enter word: ");
        Word = Console.ReadLine();

        Console.WriteLine("Enter Translate: ");
        Translation = Console.ReadLine();

        DictionariesManagement.AddWord(CurrentDictionary, Word, Translation);
    }

    public void ReplaceWith()
    {
        string Word, NewWord;
        string? Translation;

        int choice = 0;

        Console.WriteLine("What do you want to replace?\n" +
            "1. Word\n" +
            "2. Translation\n");

        while (choice < 1 || choice > 2)
        {
            Int32.TryParse(Console.ReadLine(), out choice);
        }

        DisplayDictionaryData();

        if(choice == 1)
        {
            Console.WriteLine("Enter the word to change: ");
            Word = Console.ReadLine();

            Console.WriteLine("Enter new word: ");
            NewWord = Console.ReadLine();

            DictionariesManagement.Swap(CurrentDictionary, Word, NewWord);
        }
        else
        {
            Console.WriteLine("Enter word: ");
            Word = Console.ReadLine();

            Console.WriteLine("Enter the translation you want to change: ");
            Translation = Console.ReadLine();

            Console.WriteLine("Enter new word: ");
            NewWord = Console.ReadLine();

            DictionariesManagement.Swap(CurrentDictionary, Word, NewWord, Translation);
        }
    }

    public void DeleteWord()
    {
        string Word;
        string? Translation;

        int choice = 0;

        Console.WriteLine("What do you want to remove?\n" +
            "1. Word\n" +
            "2. Translation\n");

        while (choice < 1 || choice > 2)
        {
            Int32.TryParse(Console.ReadLine(), out choice);
        }

        DisplayDictionaryData();

        if (choice == 1)
        {
            Console.WriteLine("Enter the word to remove: ");
            Word = Console.ReadLine();

            

            DictionariesManagement.RemoveWord(CurrentDictionary, Word);
        }
        else
        {
            Console.WriteLine("Enter word: ");
            Word = Console.ReadLine();

            Console.WriteLine("Enter the translation you want to remove: ");
            Translation = Console.ReadLine();

            DictionariesManagement.RemoveWord(CurrentDictionary, Word, Translation);
        }
    }

    public void SeekWord()
    {
        string Word;

        Console.WriteLine("Enter word for search: ");
        Word = Console.ReadLine();

        DictionariesManagement.FindWord(CurrentDictionary, Word);
    }

    public void DisplayDictionaryData()
    {
        DictionariesManagement.DisplayData(CurrentDictionary);
    }

    public void InCSV()
    {
        DictionariesManagement.DownloadInCSV();
    }
}