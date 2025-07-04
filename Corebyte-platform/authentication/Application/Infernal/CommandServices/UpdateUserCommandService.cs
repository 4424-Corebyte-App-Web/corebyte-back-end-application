using Corebyte_platform.authentication.Domain.Model.Aggregates;
using Corebyte_platform.authentication.Domain.Model.Commands;
using Corebyte_platform.authentication.Domain.Repositories;
using Corebyte_platform.Shared.Domain.Repositories;

namespace Corebyte_platform.alerts.Application.Infernal.CommandServices;

public class UpdateUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(UpdateUserCommand command)
    {
        var existingUser = await _userRepository.FindByIdAsync(command.Id);
        if (existingUser == null)
            throw new KeyNotFoundException($"User with ID {command.Id} not found.");

        var duplicate = await _userRepository.FindByEmailExceptIdAsync(command.Id, command.Email);
        if (duplicate != null)
            throw new InvalidOperationException($"Another user already exists with email {command.Email}");

        existingUser.Update(command.Username, command.Email, command.Password, command.Rol);
        await _userRepository.UpdateAsync(existingUser);
        await _unitOfWork.CompleteAsync();

        return existingUser;
    }
}
