using VismaShortage.BusinessLogic.Models;
using VismaShortage.DataAccess.Repositories;

namespace VismaShortage.BusinessLogic.Services;

public class ShortageService
{
    private readonly IShortageRepository _repository;
    private readonly UserService _userService;

    public ShortageService(UserService userService, IShortageRepository repository)
    {
        _userService = userService;
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
        var existingShortage = _repository.GetByRoomAndTitle(shortage.Room, shortage.Title);

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
        var existingShortage = _repository.GetByRoomAndTitle(room, title);

        if (existingShortage == null)
            return;

        if (existingShortage.Name.Equals(_userService.Username, StringComparison.OrdinalIgnoreCase)
            || _userService.Username.Equals("admin", StringComparison.OrdinalIgnoreCase))
        {
            _repository.Delete(existingShortage);
        }
    }

    /// <summary>
    /// Get all shortages
    /// If admin gets all, if user get created by user.
    /// </summary>
    /// <returns>List of shortages</returns>
    public IEnumerable<Shortage> GetAll()
    {
        return _userService.Username.Equals("admin", StringComparison.OrdinalIgnoreCase)
            ? _repository.GetAll()
            : _repository.GetAll(_userService.Username);
    }

    /// <summary>
    /// Get all shortages by category
    /// If admin gets all, if user get created by user.
    /// </summary>
    /// <param name="category"></param>
    /// <returns>List of shortages</returns>
    public IEnumerable<Shortage> GetAllByCategory(Category category)
    {
        return _userService.Username.Equals("admin", StringComparison.OrdinalIgnoreCase)
            ? _repository.GetAllByCategory(category)
            : _repository.GetAllByCategory(category, _userService.Username);
    }

    /// <summary>
    /// Get all shortages by room type
    /// If admin gets all, if user get created by user.
    /// </summary>
    /// <param name="roomType"></param>
    /// <returns>List of shortages</returns>
    public IEnumerable<Shortage> GetAllByRoomType(RoomType roomType)
    {
        return _userService.Username.Equals("admin", StringComparison.OrdinalIgnoreCase)
            ? _repository.GetAllByRoomType(roomType)
            : _repository.GetAllByRoomType(roomType, _userService.Username);
    }

    /// <summary>
    /// Get all shortages by whether the creation date is included in the specified interval.
    /// If admin gets all, if user get created by user.
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns>List of shortages</returns>
    public IEnumerable<Shortage> GetAllByCreationDateInterval(DateTime startDate, DateTime endDate)
    {
        return _userService.Username.Equals("admin", StringComparison.OrdinalIgnoreCase)
            ? _repository.GetAllByCreationDateInterval(startDate, endDate)
            : _repository.GetAllByCreationDateInterval(startDate, endDate, _userService.Username);
    }

    /// <summary>
    /// Get all shortages by title.
    /// If admin gets all, if user get created by user.
    /// </summary>
    /// <param name="title"></param>
    /// <returns>List of shortages</returns>
    public IEnumerable<Shortage> GetAllByTitle(string title)
    {
        return _userService.Username.Equals("admin", StringComparison.OrdinalIgnoreCase)
            ? _repository.GetAllByTitle(title)
            : _repository.GetAllByTitle(title, _userService.Username);
    }
}