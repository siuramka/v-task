using VismaShortage.BusinessLogic.Filters;
using VismaShortage.BusinessLogic.Models;
using VismaShortage.BusinessLogic.Services;
using VismaShortage.DataAccess.InOut;
using VismaShortage.DataAccess.Repositories;


Console.WriteLine("Please login!");
Console.WriteLine("Enter your username/name or enter admin to login as admin!");

var username = Console.ReadLine();

List<Shortage> _shortages = new ShortageJsonReader("data.json").ReadAllShortages();
ShortageJsonWriter _writer = new ShortageJsonWriter("data.json");

ShortageRepository repository = new ShortageRepository(_shortages, _writer);

var service = new ShortageService(username, repository);

Action();

void Action()
{
    Console.WriteLine("List all shortages actions - type:   1");
    Console.WriteLine("Delete a shortage - type:   2");
    Console.WriteLine("Add new shortage - type:   3");

    var action = Console.ReadLine();

    if (action == "1")
    {
        ListAllAction();
    }
    else if (action == "2")
    {
        DeleteAction();
    }
    else if (action == "3")
    {
        CreateAction();
    }
}

void PrintShortages(List<Shortage> shortages)
{
    foreach (var shortage in shortages)
    {
        Console.WriteLine(shortage);
    }

    if (!shortages.Any())
    {
        Console.WriteLine("No shortages found.");
    }
}

void ListAllAction()
{
    Console.WriteLine("List all shortages - type:   1");
    Console.WriteLine("Filter shortages - type:   2");
    var action = Console.ReadLine();

    if (action == "1")
    {
        var shortages = service.GetAll();
        PrintShortages(shortages);
        Action();
    }
    else if (action == "2")
    {
        FilterActions();
    }
}

void DeleteAction()
{
    Console.WriteLine("Enter Shortage to delete data:");
    Console.WriteLine("Title:");
    var title = Console.ReadLine();
    Console.WriteLine("Room (MeetingRoom - Type: 0, Kitchen - Type: 1, Other - Type: 2):");
    var room = Console.ReadLine();
    Enum.TryParse(room, out RoomType parsedRoom);

    service.Delete(title, parsedRoom);

    Action();
}

void FilterActions()
{
    Console.WriteLine("Enter filter:");
    Console.WriteLine("Filter by Category: type - 1");
    Console.WriteLine("Filter by Date: type - 2");
    Console.WriteLine("Filter by Room: type - 3");
    Console.WriteLine("Filter by Title: type - 4");

    var action = Console.ReadLine();
    IFilter? filter = null;

    if (action == "1")
    {
        Console.WriteLine("Category (Electronics - Type: 0, Food - Type: 1, Other - Type: 2):");
        var category = Console.ReadLine();
        Enum.TryParse(category, out Category parsedCategory);

        filter = new FilterByCategory(parsedCategory);
    }
    else if (action == "2")
    {
        Console.WriteLine("Enter start date");
        var startDate = Console.ReadLine();

        Console.WriteLine("Enter end date");
        var endDate = Console.ReadLine();

        DateTime.TryParse(startDate, out DateTime parsedStartDate);
        DateTime.TryParse(endDate, out DateTime parsedEndDate);

        filter = new FilterByCreatedDate(parsedStartDate, parsedEndDate);
    }
    else if (action == "3")
    {
        Console.WriteLine("Room (MeetingRoom - Type: 0, Kitchen - Type: 1, Other - Type: 2):");
        var category = Console.ReadLine();
        Enum.TryParse(category, out RoomType parsedRoom);

        filter = new FilterByRoom(parsedRoom);
    }
    else if (action == "4")
    {
        Console.Write("Title:");
        var title = Console.ReadLine();

        filter = new FilterByTitle(title);
    }
    else
    {
        return;
    }

    var filteredShortages = filter.Apply(service.GetAll());
    PrintShortages(filteredShortages);

    Action();
}

void CreateAction()
{
    Console.WriteLine("Please enter shortage data:");
    Console.WriteLine("Title:");
    var title = Console.ReadLine();


    Console.WriteLine("Room (MeetingRoom - Type: 0, Kitchen - Type: 1, Other - Type: 2):");
    var room = Console.ReadLine();
    Enum.TryParse(room, out RoomType parsedRoom);

    Console.WriteLine("Category (Electronics - Type: 0, Food - Type: 1, Other - Type: 2):");
    var category = Console.ReadLine();
    Enum.TryParse(category, out Category parsedCategory);

    Console.WriteLine("Priority (1-10):");
    var priority = Console.ReadLine();
    Int32.TryParse(priority, out int parsedPriority);

    var createdOn = DateTime.Now;

    Shortage newShortage = new Shortage(title, username, parsedPriority, parsedRoom, createdOn, parsedCategory);

    bool addedStatus = service.Add(newShortage);

    if (!addedStatus)
    {
        Console.WriteLine("Warning! Shortage with Title and Room already exists!");
    }
    else
    {
        Console.WriteLine("Added!");
    }

    Action();
}