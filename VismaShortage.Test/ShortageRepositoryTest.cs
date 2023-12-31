using VismaShortage.BusinessLogic.Models;
using VismaShortage.BusinessLogic.Services;
using VismaShortage.DataAccess.InOut;
using VismaShortage.DataAccess.InOut.JsonData;
using VismaShortage.DataAccess.Repositories;

namespace VismaShortage.Test;

public class ShortageRepositoryTest
{
    private ShortageFileRepository _fileRepository;
    
    [SetUp]
    public void Setup()
    {
        File.Delete("testData.json");

        IFileReader fileReader = new ShortageJsonFileReader("testData.json");
        IFileWriter fileWriter = new ShortageJsonFileWriter("testData.json");

        _fileRepository = new ShortageFileRepository(fileReader, fileWriter);
    }

    [Test]
    public void ReadAll_SortedBy_Priority_Descending_ShouldReturnFirstShortageBeTopPriority()
    {
        var shortage1 = new Shortage("TestShortage111", "admin", 8,
            0, new DateTime(2020, 11, 11), 0);
        //Shortage w highest priority
        var shortage2 = new Shortage("TestShortage222", "admin", 10,
            0, new DateTime(2020, 11, 11), 0);
        
        var shortage3 = new Shortage("TestShortage333", "admin", 8,
            0, new DateTime(2020, 11, 11), 0);
        
        _fileRepository.Create(shortage1);
        _fileRepository.Create(shortage2);
        _fileRepository.Create(shortage3);

        var shortages = _fileRepository.GetAll();
        Assert.AreEqual(shortage2, shortages.First());
    }
}