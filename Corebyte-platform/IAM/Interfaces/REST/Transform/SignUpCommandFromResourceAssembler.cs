using Corebyte_platform.IAM.Domain.Model.Commands;
using Corebyte_platform.IAM.Interfaces.REST.Resources;

namespace Corebyte_platform.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}