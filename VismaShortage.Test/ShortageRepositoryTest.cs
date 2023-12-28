using VismaShortage.BusinessLogic.Models;
using VismaShortage.BusinessLogic.Services;
using VismaShortage.DataAccess.InOut;
using VismaShortage.DataAccess.Repositories;

namespace VismaShortage.Test;

public class ShortageRepositoryTest
{
    private ShortageRepository _repository;
    
    [SetUp]
    public void Setup()
    {
        File.Delete("testData.json");

        List<Shortage> shortages = new ShortageJsonReader("testData.json").ReadAllShortages();
        ShortageJsonWriter writer = new ShortageJsonWriter("testData.json");

        _repository = new ShortageRepository(shortages, writer);
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
        
        _repository.Create(shortage1);
        _repository.Create(shortage2);
        _repository.Create(shortage3);

        var shortages = _repository.ReadAll();
        Assert.AreEqual(shortage2, shortages.First());
    }
}