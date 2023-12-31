using VismaShortage.BusinessLogic.Models;
using VismaShortage.DataAccess.InOut;

namespace VismaShortage.DataAccess.Repositories;

public class ShortageFileRepository : IShortageRepository
{
    private List<Shortage> _shortages;

    //Depend on absractions rather than concrete impolementations
    private IFileReader _fileReader;
    private IFileWriter _fileWriter;

    public ShortageFileRepository(IFileReader fileReader, IFileWriter fileWriter)
    {
        _fileReader = fileReader;
        _fileWriter = fileWriter;

        _shortages = _fileReader.ReadShortages().ToList();
    }

    public void Create(Shortage shortage)
    {
        _shortages.Add(shortage);
        Save();
    }

    public void Update(Shortage shortage, Shortage newShortage)
    {
        _shortages.Remove(shortage);
        _shortages.Add(newShortage);
        Save();
    }

    public void Delete(Shortage shortage)
    {
        _shortages.Remove(shortage);
        Save();
    }

    public IEnumerable<Shortage> GetAll()
    {
        return _shortages.OrderByDescending(s => s.Priority);
    }

    public IEnumerable<Shortage> GetAll(string username)
    {
        return _shortages.Where(s => s.Name.Equals(username, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(s => s.Priority);
    }

    public IEnumerable<Shortage> GetAllByCategory(Category category)
    {
        return _shortages.Where(s => s.Category.Equals(category));
    }


    public IEnumerable<Shortage> GetAllByCategory(Category category, string username)
    {
        return _shortages.Where(s =>
            s.Category.Equals(category) && s.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Shortage> GetAllByTitle(string title)
    {
        return _shortages.Where(s => s.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Shortage> GetAllByTitle(string title, string username)
    {
        return _shortages.Where(s =>
            s.Title.Contains(title, StringComparison.OrdinalIgnoreCase) &&
            s.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Shortage> GetAllByCreationDateInterval(DateTime startDate, DateTime endDate)
    {
        return _shortages.Where(s => startDate > s.CreatedOn && s.CreatedOn < endDate);
    }

    public IEnumerable<Shortage> GetAllByCreationDateInterval(DateTime startDate, DateTime endDate,
        string username)
    {
        return _shortages.Where(s =>
            startDate > s.CreatedOn && s.CreatedOn < endDate &&
            s.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Shortage> GetAllByRoomType(RoomType roomType)
    {
        return _shortages.Where(s =>
            s.Room.Equals(roomType));
    }

    public IEnumerable<Shortage> GetAllByRoomType(RoomType roomType, string username)
    {
        return _shortages.Where(s =>
            s.Room.Equals(roomType) && s.Name.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public Shortage? GetByRoomAndTitle(RoomType room, string title)
    {
        return _shortages.FirstOrDefault(s =>
            s.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && s.Room.Equals(room));
    }

    private void Save()
    {
        _fileWriter.WriteShortages(_shortages);
    }
}