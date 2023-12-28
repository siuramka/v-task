using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.BusinessLogic.Filters;

public class FilterByTitle : IFilter
{
    private string _title { get; set; }
    
    /// <summary>
    /// Filter by shortage title
    /// </summary>
    /// <param name="title">Title of shortage</param>
    public FilterByTitle(string title)
    {
        _title = title;
    }
    
    public List<Shortage> Apply(List<Shortage> shortages)
    {
        return shortages.Where(s => s.Title.Contains(_title, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}