using VismaShortage.BusinessLogic.Models;

namespace VismaShortage.DataAccess.InOut;

public interface IFileWriter
{
    void WriteShortages(IEnumerable<Shortage> shortages);
}