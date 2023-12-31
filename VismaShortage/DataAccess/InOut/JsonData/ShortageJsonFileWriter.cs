using System.Text.Json;
using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.DataAccess.InOut.JsonData;

public class ShortageJsonFileWriter : IFileWriter
{
    private string _jsonFilePath;

    public ShortageJsonFileWriter(string jsonFilePath)
    {
        _jsonFilePath = jsonFilePath;
    }

    /// <summary>
    /// Write shortages to file in json format
    /// </summary>
    /// <param name="shortages"></param>
    public void WriteShortages(IEnumerable<Shortage> shortages)
    {
        var shortagesJson = JsonSerializer.Serialize(shortages);
        File.WriteAllText(_jsonFilePath, shortagesJson);
    }
}