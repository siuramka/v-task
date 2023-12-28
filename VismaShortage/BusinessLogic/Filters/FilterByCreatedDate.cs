using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.BusinessLogic.Filters;

public class FilterByCreatedDate : IFilter
{
    private DateTime _startDate;
    private DateTime _endDate;

    /// <summary>
    /// Filter by whether the creation date falls within the start and end date interval.
    /// </summary>
    /// <param name="startDate">Start date of the inverval</param>
    /// <param name="endDate">End date of the interval</param>
    public FilterByCreatedDate(DateTime startDate, DateTime endDate)
    {
        _startDate = startDate.ToUniversalTime();
        _endDate = endDate.ToUniversalTime();
    }

    public List<Shortage> Apply(List<Shortage> shortages)
    {
        return shortages.Where(s => _startDate < s.CreatedOn && _endDate > s.CreatedOn).ToList();
    }
}