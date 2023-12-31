using VismaShortage.BusinessLogic.Models;
using VismaShortage.BusinessLogic.Services;
using VismaShortage.DataAccess.InOut;
using VismaShortage.DataAccess.InOut.JsonData;
using VismaShortage.DataAccess.Repositories;

namespace VismaShortage.Test;

public class ShortageServiceTest
{
    private ShortageService _service;
    private ShortageFileRepository _fileRepository;

    [SetUp]
    public void Setup()
    {
        File.Delete("testData.json");

        IFileReader fileReader = new ShortageJsonFileReader("testData.json");
        IFileWriter fileWriter = new ShortageJsonFileWriter("testData.json");

        _fileRepository = new ShortageFileRepository(fileReader, fileWriter);

        _service = new ShortageService(new UserService("admin"), _fileRepository);
    }

    [Test]
    public void AddShortage_ValidShortage_ShouldAdd()
    {
        var shortage = new Shortage("TestShortage", "admin", 10,
            0, new DateTime(2020, 11, 11), 0);

        _service.Add(shortage);

        Assert.Contains(shortage, _service.GetAll().ToList());
    }

    [Test]
    public void AddShortage_InValidShortage_ShouldNotAdd()
    {
        var shortage = new Shortage("TestShortage", "admin", 10,
            0, new DateTime(2020, 11, 11), 0);

        _service.Add(shortage);
        _service.Add(shortage);

        Assert.AreEqual(1, _service.GetAll().Count());
    }

    [Test]
    public void DeleteShortage_Admin_ValidShortage_ShouldShortagesBeEmpty()
    {
        var shortage = new Shortage("TestShortage", "admin", 10,
            0, new DateTime(2020, 11, 11), 0);

        _service.Add(shortage);
        _service.Delete(shortage.Title, shortage.Room);

        Assert.False(_service.GetAll().Any());
    }

    [Test]
    public void DeleteShortage_Others_ValidShortage_ShouldNotDeleteOtherUsersShortage()
    {
        var adminShortage = new Shortage("TestShortage111", "admin", 10,
            0, new DateTime(2020, 11, 11), 0);

        _service.Add(adminShortage);
        
        _service = new ShortageService(new UserService("user"), _fileRepository);

        var userShortage = new Shortage("TestShortage222", "user", 10,
            0, new DateTime(2020, 11, 11), 0);

        _service.Add(userShortage);
        
        _service.Delete(adminShortage.Title, adminShortage.Room);
        
        _service = new ShortageService(new UserService("admin"), _fileRepository);

        //Admin shortage still contains
        Assert.Contains(adminShortage, _service.GetAll().ToList());
    }
}