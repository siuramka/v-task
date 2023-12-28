using VismaShortage.BusinessLogic.Filters;
using VismaShortage.BusinessLogic.Models;
using VismaShortage.BusinessLogic.Services;
using VismaShortage.DataAccess.InOut;
using VismaShortage.DataAccess.Repositories;

namespace VismaShortage.Test;

public class FilterTest
{
    private ShortageService _service;

    [SetUp]
    public void Setup()
    {
        File.Delete("testData.json");

        List<Shortage> shortages = new ShortageJsonReader("testData.json").ReadAllShortages();
        ShortageJsonWriter writer = new ShortageJsonWriter("testData.json");

        var repository = new ShortageRepository(shortages, writer);
        _service = new ShortageService("admin", repository);
    }

    [Test]
    public void FilterBy_CreateDate_ShouldReturnShortagesMathingByDateInterval()
    {
        var shortage1 = new Shortage("TestShortage111", "admin", 10,
            RoomType.MeetingRoom, new DateTime(2020, 11, 11), Category.Food);
        var shortage2 = new Shortage("TestShortage222", "admin", 10, RoomType.Bathroom, new DateTime(2020, 11, 13),
            Category.Electronics);

        _service.Add(shortage1);
        _service.Add(shortage2);

        IFilter filterByDates = new FilterByCreatedDate(new DateTime(2020, 10, 10), new DateTime(2020, 11, 14));
        var filteredShortages = filterByDates.Apply(_service.GetAll());

        Assert.AreEqual(2, filteredShortages.Count);
    }

    [Test]
    public void FilterBy_Title_ShouldReturnShortagesMatchingTitle()
    {
        var shortage1 = new Shortage("Shortage Very Cool Stuff", "admin", 10,
            RoomType.MeetingRoom, new DateTime(2020, 11, 11), Category.Food);
        var shortage2 = new Shortage("VERY COOLCOOL STUFF YEAH MAN", "admin", 10, RoomType.Bathroom,
            new DateTime(2020, 11, 13),
            Category.Electronics);

        _service.Add(shortage1);
        _service.Add(shortage2);

        IFilter filterByTitle = new FilterByTitle("cool");
        var filteredShortages = filterByTitle.Apply(_service.GetAll());

        Assert.AreEqual(2, filteredShortages.Count);
    }
}