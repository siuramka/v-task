using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.DataAccess.InOut;

public interface IFileReader
{
    IEnumerable<Shortage> ReadShortages();
}