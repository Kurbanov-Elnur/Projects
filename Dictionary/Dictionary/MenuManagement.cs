using MyDictionaries;
using System.Text;

namespace Management;

class MenuManagement
{
    public DictionariesManagement DictionariesManagement = new();
    public Favorites Favorites = new();
    public ushort CurrentDictionary { get; private set; }

    public void SelectCurrentDictionary()
    {
        int choice = -1;

        if (DictionariesManagement.Dictionaries.Count > 0)
        {
            Console.WriteLine("Select current dictionary: ");
            Console.Write(DictionariesManagement);
            Console.WriteLine("Enter 0 for create new dictionary.");

            CheckChoice(0, DictionariesManagement.Dictionaries.Count, ref choice);

            if(choice == 0)
            {
                CreateDictionary();
                CurrentDictionary = (ushort)(DictionariesManagement.Dictionaries.Count - 1);
                return; 
            }

            CurrentDictionary = (ushort)(choice - 1);
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
        int choice = 0;

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

        CheckChoice(1, languages.Count, ref choice);

        dictionaryType[0] = languages[choice - 1];

        languages.Remove(languages[choice - 1]);
        choice = 0;

        Console.WriteLine("Select the translation language: ");
        for (int i = 0; i < languages.Count; i++)
            Console.WriteLine($"{i + 1}. {languages[i]}");

        CheckChoice(1, languages.Count, ref choice);

        dictionaryType[1] = languages[choice - 1];

        try
        {
            DictionariesManagement.AddNewDictionary(dictionaryType);
        }
        catch(Exception e) 
        {
            Console.WriteLine(e.Message);
        }
    }

    public void AddWord()
    {
        string Word, Translation;

        Console.WriteLine("Enter word: ");
        Word = Console.ReadLine();

        Console.WriteLine("Enter Translate: ");
        Translation = Console.ReadLine();

        DictionariesManagement.AddWord(CurrentDictionary, Word, Translation);
    }

    public void AddFavoriteWord()
    {
        string word;

        DictionariesManagement.DisplayData(CurrentDictionary);

        Console.WriteLine("Enter favorite word: ");
        word = Console.ReadLine();

        try
        {
            Favorites.AddWord(word, DictionariesManagement.Dictionaries[CurrentDictionary].Dictionary[word]);
        }
        catch (Exception e)
        {
            Console.WriteLine("There is no such word in the dictionary!");
        }
    }

    public void ReplaceWord()
    {
        string Word, NewWord;
        string? Translation;

        int choice = 0;

        Console.WriteLine("What do you want to replace?\n" +
            "1. Word\n" +
            "2. Translation");

        CheckChoice(1, 2, ref choice);


        DisplayDictionaryData();

        if(choice == 1)
        {
            Console.WriteLine("Enter the word to change: ");
            Word = Console.ReadLine();

            Console.WriteLine("Enter new word: ");
            NewWord = Console.ReadLine();

            DictionariesManagement.ReplaceWord(CurrentDictionary, Word, NewWord);
        }
        else
        {
            Console.WriteLine("Enter word: ");
            Word = Console.ReadLine();

            Console.WriteLine("Enter the translation you want to change: ");
            Translation = Console.ReadLine();

            Console.WriteLine("Enter new word: ");
            NewWord = Console.ReadLine();

            DictionariesManagement.ReplaceWord(CurrentDictionary, Word, NewWord, Translation);
        }
    }

    public void DeleteWord()
    {
        string Word;
        string? Translation;

        int choice = 0;

        Console.WriteLine("What do you want to remove?\n" +
            "1. Word\n" +
            "2. Translation");

        CheckChoice(1, 2, ref choice);

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

    public void RemoveFavoriteWord()
    {
        string word;

        Favorites.DisplayData();

        Console.WriteLine("Enter favorite word for remove: ");
        word = Console.ReadLine();

        try
        {
            Favorites.RemoveWord(word);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void SearchWord()
    {
        string Word;

        Console.WriteLine("Enter word for search: ");
        Word = Console.ReadLine();

        DictionariesManagement.SearchWord(CurrentDictionary, Word);
    }

    public void DisplayCurrentDictioany()
    {
        Console.WriteLine($"Current dictionary: {DictionariesManagement.Dictionaries[CurrentDictionary]}\n");
    }

    public void DisplayDictionaryData()
    {
        DictionariesManagement.DisplayData(CurrentDictionary);
    }

    public void DisplayFavoriteWords()
    {
        Favorites.DisplayData();
    }

    public void CheckChoice(int start, int end, ref int choice)
    {
        Int32.TryParse(Console.ReadLine(), out choice);
        while (choice < start || choice > end)
        {
            Console.Write("Wrong choice!\n" +
                "Please re-enter: ");
            Int32.TryParse(Console.ReadLine(), out choice);
        }
    }

    public void ExportInCSV()
    {
        try
        {
            foreach (var pair in Favorites.FavoritesWords)
            {
                using (var writer = new StreamWriter("FavoriteWords.csv", false, Encoding.UTF8))
                {
                    string key = pair.Key;
                    string values = string.Join(",", pair.Value);
                    writer.WriteLine($"{key},{values}");
                }
            }

            DictionariesManagement.ExportInCSV();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void ImportInProgram()
    {
        try
        {
            using (var reader = new StreamReader("FavoriteWords.csv"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    string key = parts[0];
                    List<string> values = parts.Skip(1).ToList();

                    Favorites.AddWord(key, values);
                }
            }

            DictionariesManagement.ImportInProgram();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}