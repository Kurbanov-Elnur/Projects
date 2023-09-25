using MyDictionaries;
using System.Text;
using System.Threading.Channels;

namespace Management;

class DictionariesManagement
{
    public List<MyDictionary> Dictionaries;
    public List<string[]> DictionaryTypes;

    public DictionariesManagement()
    {
        Dictionaries = new ();
        DictionaryTypes = new();
    }

    public void AddNewDictionary(string[] dictionaryType)
    {
        if(dictionaryType.Length != 2)
        {
            throw new Exception("Invalid argument!");
        }

        for (int i = 0; i < Dictionaries.Count; i++)
        {
            if (Dictionaries[i].DictionaryType[0] == dictionaryType[0] && Dictionaries[i].DictionaryType[1] == dictionaryType[1])
                throw new Exception("Such a dictionary already exists");
        }

        Dictionaries.Add(new MyDictionary(dictionaryType));
        DictionaryTypes.Add(dictionaryType);
    }

    public void AddWord(ushort CurrentDictionary, string Word, string Translation)
    {
        try
        {
            Dictionaries[CurrentDictionary].AppendWord(Word, Translation);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void Swap(ushort CurrentDictionary, string Word, string NewWord, string? Translation = null)
    {
        if(Translation != null)
        {
            Dictionaries[CurrentDictionary].ReplaceWord(Word, NewWord, Translation);
        }
        else
            Dictionaries[CurrentDictionary].ReplaceWord(Word, NewWord);
    }

    public void RemoveWord(ushort CurrentDictionary, string Word, string? Translation = null)
    {
        if (Translation != null)
        {
            Dictionaries[CurrentDictionary].RemoveWord(Word, Translation);
        }
        else
            Dictionaries[CurrentDictionary].RemoveWord(Word);
    }

    public void FindWord(ushort CurrentDictionary, string Word)
    {
        Dictionaries[CurrentDictionary].SearchWord(Word);
    }

    public void DisplayData(ushort CurrentDictionary)
    {
        Dictionaries[CurrentDictionary].DisplayData();
    }

    public void DownloadInCSV()
    {
        foreach (var item in Dictionaries)
        {
            string FileName = item.DictionaryType[0] + "-" + item.DictionaryType[0] + ".csv";

            using (var writer = new StreamWriter(FileName, false, Encoding.UTF8))
            {
                foreach (var pair in item.Dictionary)
                {
                    string key = pair.Key;
                    string values = string.Join(",", pair.Value);
                    writer.WriteLine($"{key},{values}");
                }
            }
        }
    }

    public override string ToString()
    {
        StringBuilder ListOfDictionaries = new();

        for (int i = 0; i < DictionaryTypes.Count; i++)
        {
            ListOfDictionaries.Append($"{i + 1}. {DictionaryTypes[i][0]} - {DictionaryTypes[i][1]}\n");
        }

        return ListOfDictionaries.ToString();
    }
}