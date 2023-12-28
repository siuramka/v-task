using VismaShortage.BusinessLogic.Models;
using VismaShortage.DataAccess.Repositories;

namespace VismaShortage.BusinessLogic.Services;

public class ShortageService
{
    private readonly ShortageRepository _repository;
    private string _currentUserName;

    public ShortageService(string username, ShortageRepository repository)
    {
        _currentUserName = username;
        _repository = repository;
    }

    /// <summary>
    /// Adds shortage if it doesnt exist.
    /// If shortage exists and has lower priority then new one overrides it.
    /// </summary>
    /// <param name="shortage"></param>
    /// <returns>True if created/overriden False if Shortage already exists</returns>
    public bool Add(Shortage shortage)
    {
        var existingShortage = _repository.ReadByTitleAndRoom(shortage.Title, shortage.Room);

        if (existingShortage == null)
        {
            _repository.Create(shortage);
            return true;
        }

        if (shortage.Priority > existingShortage.Priority)
        {
            _repository.Update(existingShortage, shortage);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Deletes shortage if creator of shortage or user is admin
    /// </summary>
    /// <param name="title">Title of shortage</param>
    /// <param name="room">Room type of shortage</param>
    public void Delete(string title, RoomType room)
    {
        var shortage = _repository.ReadByTitleAndRoom(title, room);

        if (shortage == null)
            return;

        if (shortage.Name.Equals(_currentUserName, StringComparison.OrdinalIgnoreCase)
            || _currentUserName.Equals("admin", StringComparison.OrdinalIgnoreCase))
        {
            _repository.Delete(shortage);
        }
    }

    /// <summary>
    /// Retrieves shortages based on user.
    /// If user retrieves own shortages.
    /// If admin retrieves all shortages.
    /// </summary>
    /// <returns>List of shortages</returns>
    public List<Shortage> GetAll()
    {
        return _currentUserName.Equals("admin", StringComparison.OrdinalIgnoreCase)
            ? _repository.ReadAll()
            : _repository.ReadAllByUser(_currentUserName);
    }
}