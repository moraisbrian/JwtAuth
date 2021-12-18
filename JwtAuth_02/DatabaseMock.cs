using JwtAuth.Models;

public class DatabaseMock 
{
    public static List<UserModel> GetUsers() 
    {
        var users = new List<UserModel>();

        users.Add(new UserModel
        {
            UserName = "fulano.detal",
            EmailAddress = "fulano@gmail.com",
            GivenName = "Fulano",
            Password = "password",
            Role = "Admin",
            Surname = "De Tal"
        });

        users.Add(new UserModel
        {
            UserName = "deutrano.xpto",
            EmailAddress = "dutrano@gmail.com",
            GivenName = "Deutrano",
            Password = "password",
            Role = "User",
            Surname = "Xpto"
        });

        return users;
    }
}