using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.BusinessLogic.Filters;

public interface IFilter
{
    /// <summary>
    /// Apply current filter to shortages
    /// </summary>
    /// <param name="shortages"></param>
    /// <returns>Filtered shortages</returns>
    public List<Shortage> Apply(List<Shortage> shortages);
}