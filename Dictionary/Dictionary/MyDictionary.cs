using MyRegex;
namespace MyDictionaries;

class MyDictionary
{
    public string[] DictionaryType { get; init; }
    public Dictionary<string, List<string>> Dictionary { get; set; }

    public MyDictionary(string[] dictionaryType)
    {
        if (dictionaryType.Length == 2)
        {
            this.DictionaryType = dictionaryType;
            this.Dictionary = new();
        }
        else throw new ArgumentException("The array must contain exactly two elements.", nameof(dictionaryType));
    }

    public void AppendWord(string Word, string Translate)
    {
        try
        {
            CheckWord(Word, DictionaryType[0]);
            CheckWord(Translate, DictionaryType[1]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        if (!Dictionary.ContainsKey(Word))
        {
            Dictionary[Word] = new List<string>();
        }
        Dictionary[Word].Add(Translate);
    }

    public void ReplaceWord(string Word, string NewWord, string? Translation = null)
    {
        try
        {
            if(Translation != null)
            {
                try 
                {
                    CheckWord(Translation, DictionaryType[1]);
                    CheckWord(NewWord, DictionaryType[1]);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Dictionary[Word][Dictionary[Word].IndexOf(Translation)] = NewWord;
            }
            else 
            {
                try
                {
                    CheckWord(Word, DictionaryType[0]);
                    CheckWord(NewWord, DictionaryType[0]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                List<string> temp = Dictionary[Word];
                Dictionary.Remove(Word);
                Dictionary[NewWord] = temp;
            }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void RemoveWord(string Word, string? Translation = null)
    {
        try
        {
            if (Translation != null)
            {
                try
                {
                    CheckWord(Translation, DictionaryType[1]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Dictionary[Word].Remove(Translation);
            }
            else
            {
                try
                {
                    CheckWord(Word, DictionaryType[0]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Dictionary.Remove(Word);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void SearchWord(string Word)
    {
        try
        {
            for (int i = 0; i < Dictionary[Word].Count; i++)
            {
                Console.WriteLine($"Translation {i + 1}: {Dictionary[Word][i]}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void DisplayData()
    {
        foreach (var key in Dictionary.Keys)
        {
            Console.Write($"{key} - ");
            for (int i = 0; i < Dictionary[key].Count; i++)
            {
                Console.Write($"{Dictionary[key][i]}");
                if(i + 1 != Dictionary[key].Count)
                    Console.Write(", ");
            }
            Console.WriteLine();
        }
    }

    private void CheckWord(string Word, string Language)
    {
        if (!СheckRegex.Сheck(Word, Language))
        {
            throw new ArgumentException("Invalid data!");
        }
    }
}