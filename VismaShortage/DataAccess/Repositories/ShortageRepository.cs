using VismaShortage.BusinessLogic.Models;
using VismaShortage.DataAccess.InOut;

namespace VismaShortage.DataAccess.Repositories;

public class ShortageRepository
{
    private List<Shortage> _shortages;
    private ShortageJsonWriter _writer;

    public ShortageRepository(List<Shortage> shortages, ShortageJsonWriter writer)
    {
        _shortages = shortages;
        _writer = writer;
    }

    /// <summary>
    /// Save a shortage
    /// </summary>
    /// <param name="shortage"></param>
    public void Create(Shortage shortage)
    {
        _shortages.Add(shortage);
        Save();
    }

    /// <summary>
    /// Update shortage to new shortage
    /// </summary>
    /// <param name="shortage">Old shortage to update/replace</param>
    /// <param name="newShortage">New shortage adata</param>
    public void Update(Shortage shortage, Shortage newShortage)
    {
        _shortages.Remove(shortage);
        _shortages.Add(newShortage);
        Save();
    }

    /// <summary>
    /// Reads shortage by room type and title
    /// </summary>
    /// <param name="title"></param>
    /// <param name="room"></param>
    /// <returns>Matching shortage if exists</returns>
    public Shortage? ReadByTitleAndRoom(string title, RoomType room)
    {
        return _shortages.FirstOrDefault(s =>
            s.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && s.Room.Equals(room));
    }

    /// <summary>
    /// Read all shortages
    /// </summary>
    /// <returns>Ordered shortage list by priority</returns>
    public List<Shortage> ReadAll()
    {
        return _shortages.OrderByDescending(s => s.Priority).ToList();
    }

    /// <summary>
    /// Reads all shortages of which the user has created
    /// </summary>
    /// <param name="username">Username of shortage creator</param>
    /// <returns>Ordered shortage list by priority</returns>
    public List<Shortage> ReadAllByUser(string username)
    {
        return _shortages.Where(s => s.Name.Equals(username, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(s => s.Priority)
            .ToList();
    }

    /// <summary>
    /// Delete a shortage
    /// </summary>
    /// <param name="shortage">Shortage to delete</param>
    public void Delete(Shortage shortage)
    {
        _shortages.Remove(shortage);
        Save();
    }

    /// <summary>
    /// Saves all shortage changes to JSON file
    /// </summary>
    private void Save()
    {
        _writer.WriteShortagesToFileAsJson(_shortages);
    }
}