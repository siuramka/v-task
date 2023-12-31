using System.Text.Json;
using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.DataAccess.InOut.JsonData;

public class ShortageJsonFileReader : IFileReader
{
    private string _jsonFilePath;

    public ShortageJsonFileReader(string jsonFilePath)
    {
        _jsonFilePath = jsonFilePath;
    }

    /// <summary>
    /// Read shortages from JSON file format.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Shortage> ReadShortages()
    {
        if (!File.Exists(_jsonFilePath))
        {
            return new List<Shortage>();
        }

        string storagesJsonData = File.ReadAllText(_jsonFilePath);

        var deserializedData = JsonSerializer.Deserialize<IEnumerable<Shortage>>(storagesJsonData);

        return deserializedData;
    }
}