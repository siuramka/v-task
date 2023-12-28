namespace VismaShortage.BusinessLogic.Models;

public class Shortage
{
    public string Title { get; }
    public string Name { get; }
    public int Priority { get; }
    public RoomType Room { get; }
    public DateTime CreatedOn { get; }
    public Category Category { get; }

    public Shortage(string title, string name, int priority, RoomType room, DateTime createdOn, Category category)
    {
        Title = title;
        Name = name;
        Priority = priority;
        Room = room;
        CreatedOn = createdOn.ToUniversalTime();
        Category = category;
    }

    public override string ToString()
    {
        string priorityString;

        if (Priority == 1)
            priorityString = "Not Important";
        else if (Priority == 10)
            priorityString = "Very Important";
        else
            priorityString = Priority.ToString();

        return
            $"Title: {Title} | Name: {Name} | Priority: {priorityString} | Room: {Room} | Created On: {CreatedOn} | Category: {Category}";
    }

    /// <summary>
    /// Generated overrides
    /// </summary>
    private sealed class ShortageEqualityComparer : IEqualityComparer<Shortage>
    {
        public bool Equals(Shortage x, Shortage y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return string.Equals(x.Title, y.Title, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(x.Name, y.Name, StringComparison.OrdinalIgnoreCase) && x.Priority == y.Priority &&
                   x.Room == y.Room && x.CreatedOn.Equals(y.CreatedOn) && x.Category == y.Category;
        }

        public int GetHashCode(Shortage obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Title, StringComparer.OrdinalIgnoreCase);
            hashCode.Add(obj.Name, StringComparer.OrdinalIgnoreCase);
            hashCode.Add(obj.Priority);
            hashCode.Add((int)obj.Room);
            hashCode.Add(obj.CreatedOn);
            hashCode.Add((int)obj.Category);
            hashCode.Add(obj.Name, StringComparer.OrdinalIgnoreCase);
            return hashCode.ToHashCode();
        }
    }

    public static IEqualityComparer<Shortage> ShortageComparer { get; } = new ShortageEqualityComparer();
}