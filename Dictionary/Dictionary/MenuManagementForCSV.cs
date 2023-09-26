using MyDictionaries;
using System.Text;

namespace Management;

partial class MenuManagement
{
    public void ExportInCSV()
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

    public void ImportInProgram()
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
}
