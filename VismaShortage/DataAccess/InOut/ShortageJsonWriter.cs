using System.Text.Json;
using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.DataAccess.InOut;

public class ShortageJsonWriter
{
    private string _jsonFilePath;

    public ShortageJsonWriter(string jsonFilePath)
    {
        _jsonFilePath = jsonFilePath;
    }

    /// <summary>
    /// Write shortages to file in json format
    /// </summary>
    /// <param name="shortages"></param>
    public void WriteShortagesToFileAsJson(List<Shortage> shortages)
    {
        var shortagesJson = ConvertShortagesToJsonString(shortages);
        File.WriteAllText(_jsonFilePath, shortagesJson);
    }

    /// <summary>
    /// Convert list of shortages to json object list string
    /// </summary>
    /// <param name="shortages"></param>
    /// <returns></returns>
    private string ConvertShortagesToJsonString(List<Shortage> shortages)
    {
        return JsonSerializer.Serialize(shortages);
    }
}