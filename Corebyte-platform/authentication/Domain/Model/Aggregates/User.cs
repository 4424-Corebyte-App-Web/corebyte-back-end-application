namespace Corebyte_platform.authentication.Domain.Model.Aggregates;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Rol { get; set; } = default!; // NUEVO

    public void Update(string username, string email, string password,string rol)
    {
        Username = username;
        Email = email;
        Password = password;
        Rol = rol;
    }
}
