using Corebyte_platform.orders.Domain.Model.Commands;
using Corebyte_platform.orders.Interfaces.REST.Resources;

namespace Corebyte_platform.orders.Interfaces.REST.Transform
{
    public static class DeleteOrderCommandFromResourceAssembler
    {
        public static DeleteOrdersByIdCommand ToCommandFromResource(DeleteOrderResource resource)
        {
            return new DeleteOrdersByIdCommand(resource.Id);
        }
    }
}
