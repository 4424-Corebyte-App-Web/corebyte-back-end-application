using Corebyte_platform.authentication.Domain.Model.Commands;
using Corebyte_platform.authentication.Domain.Model.Dtos;
using Corebyte_platform.authentication.Domain.Repositories;

namespace Corebyte_platform.authentication.Application.Infernal.QueryServices;

public class SearchUsersQueryService
{
    private readonly IUserRepository _userRepository;

    public SearchUsersQueryService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserDto>> Handle(SearchUsersByNameQuery query)
    {
        var users = await _userRepository.SearchByNameAsync(query.Name);
        return users.Select(u => new UserDto(u.Username, u.Email));
    }
}