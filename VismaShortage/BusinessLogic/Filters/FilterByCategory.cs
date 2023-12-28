using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.BusinessLogic.Filters;

public class FilterByCategory : IFilter
{
    private Category _category;
    
    /// <summary>
    /// Filter by shortage category
    /// </summary>
    /// <param name="category"></param>
    public FilterByCategory(Category category)
    {
        _category = category;
    }
    
    public List<Shortage> Apply(List<Shortage> shortages)
    {
        return shortages.Where(s => s.Category.Equals(_category)).ToList();
    }
}