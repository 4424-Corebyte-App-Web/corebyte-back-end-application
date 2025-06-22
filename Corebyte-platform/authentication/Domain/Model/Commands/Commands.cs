namespace Corebyte_platform.authentication.Domain.Model.Commands;

public record CreateUserCommand(string Username, string Email, string Password);
public record UpdateUserCommand(int Id, string Username, string Email, string Password);
public record DeleteUserCommand(int Id);
public record SearchUsersByNameQuery(string Name);
public record GetUserByIdQuery(int Id);