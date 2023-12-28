using System.Text.Json;
using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.DataAccess.InOut;

public class ShortageJsonReader
{
    private string _jsonFilePath;

    public ShortageJsonReader(string jsonFilePath)
    {
        _jsonFilePath = jsonFilePath;
    }

    public List<Shortage> ReadAllShortages()
    {
        try
        {
            string storagesJsonData = File.ReadAllText(_jsonFilePath);

            var deserializedData = JsonSerializer.Deserialize<List<Shortage>>(storagesJsonData);

            return deserializedData;
        }
        catch
        {
            return new List<Shortage>();
        }
    }
}