using Corebyte_platform.IAM.Domain.Model.Commands;
using Corebyte_platform.IAM.Interfaces.REST.Resources;

namespace Corebyte_platform.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}