using MyDictionaries;

namespace Management;

partial class MenuManagement
{
    public Favorites Favorites = new();

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

    public void DisplayFavoriteWords()
    {
        Favorites.DisplayData();
    }
}
