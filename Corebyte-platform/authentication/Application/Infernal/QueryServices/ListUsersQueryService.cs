using Corebyte_platform.authentication.Domain.Model.Dtos;
using Corebyte_platform.authentication.Domain.Repositories;

namespace Corebyte_platform.authentication.Application.Infernal.QueryServices;

public class ListUsersQueryService
{
    private readonly IUserRepository _userRepository;

    public ListUsersQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> Handle()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDto(u.Username, u.Email));
    }
}
