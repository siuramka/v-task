using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.DataAccess.Repositories;

public interface IShortageRepository
{
    void Create(Shortage shortage);
    void Update(Shortage shortage, Shortage newShortage);
    void Delete(Shortage shortage);
    IEnumerable<Shortage> GetAll();
    IEnumerable<Shortage> GetAll(string username);
    IEnumerable<Shortage> GetAllByCategory(Category category);
    IEnumerable<Shortage> GetAllByCategory(Category category, string username);
    IEnumerable<Shortage> GetAllByTitle(string title);
    IEnumerable<Shortage> GetAllByTitle(string title, string username);
    IEnumerable<Shortage> GetAllByCreationDateInterval(DateTime startDate, DateTime endDate);

    IEnumerable<Shortage> GetAllByCreationDateInterval(DateTime startDate, DateTime endDate,
        string username);

    IEnumerable<Shortage> GetAllByRoomType(RoomType roomType);
    IEnumerable<Shortage> GetAllByRoomType(RoomType roomType, string username);
    Shortage? GetByRoomAndTitle(RoomType room, string title);
}