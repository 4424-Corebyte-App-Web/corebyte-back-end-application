using Corebyte_platform.authentication.Domain.Model.Commands;
using Corebyte_platform.authentication.Domain.Model.Dtos;
using Corebyte_platform.authentication.Domain.Repositories;

namespace Corebyte_platform.authentication.Application.Infernal.QueryServices;

public class GetUserByIdQueryService
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery query)
    {
        var user = await _userRepository.FindByIdAsync(query.Id);
        return user == null ? null : new UserDto(user.Username, user.Email);
    }
}
