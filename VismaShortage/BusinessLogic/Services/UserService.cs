namespace VismaShortage.BusinessLogic.Services;

public class UserService
{
    public string Username { get; }

    public UserService(string username)
    {
        Username = username;
    }
}