using Corebyte_platform.authentication.Domain.Exceptions;
using Corebyte_platform.authentication.Domain.Model.Aggregates;
using Corebyte_platform.authentication.Domain.Model.Commands;
using Corebyte_platform.authentication.Domain.Repositories;
using Corebyte_platform.Shared.Domain.Repositories;
using System;

namespace Corebyte_platform.alerts.Application.Infernal.CommandServices;

public class DeleteUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(DeleteUserCommand command)
    {
        var user = await _userRepository.FindByIdAsync(command.Id);
        if (user == null) return null;

        await _userRepository.DeleteByIdAsync(command.Id);
        await _unitOfWork.CompleteAsync();

        return user;
    }
}