using Corebyte_platform.IAM.Domain.Model.Aggregates;
using Corebyte_platform.IAM.Interfaces.REST.Resources;

namespace Corebyte_platform.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(
        User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
}