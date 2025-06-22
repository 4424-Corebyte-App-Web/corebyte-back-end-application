using Corebyte_platform.authentication.Domain.Exceptions;
using Corebyte_platform.authentication.Domain.Model.Aggregates;
using Corebyte_platform.authentication.Domain.Model.Commands;
using Corebyte_platform.authentication.Domain.Repositories;
using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.alerts.Application.Infernal.CommandServices;

public class CreateUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(CreateUserCommand command)
    {
        var existing = await _userRepository.FindByEmailAsync(command.Email);
        if (existing != null)
            throw new DuplicateUserException($"User with email '{command.Email}' already exists.");

        var user = new User
        {
            Username = command.Username,
            Email = command.Email,
            Password = command.Password
        };

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        return user;
    }
}