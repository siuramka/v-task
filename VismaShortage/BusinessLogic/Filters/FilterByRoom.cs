using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.BusinessLogic.Filters;

public class FilterByRoom : IFilter
{
    private RoomType _room;
    
    /// <summary>
    /// Filter by shortage room type
    /// </summary>
    /// <param name="room">Type of room</param>
    public FilterByRoom(RoomType room)
    {
        _room = room;
    }
    
    public List<Shortage> Apply(List<Shortage> shortages)
    {
        return shortages.Where(s => s.Room.Equals(_room)).ToList();
    }
}