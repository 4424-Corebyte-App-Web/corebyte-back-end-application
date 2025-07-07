namespace Corebyte_platform.authentication.Domain.Model.Aggregates;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public void Update(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }
}