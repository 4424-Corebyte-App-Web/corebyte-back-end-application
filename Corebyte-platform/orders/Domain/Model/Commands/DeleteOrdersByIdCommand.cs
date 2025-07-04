using System.Runtime.InteropServices.Marshalling;

namespace Corebyte_platform.orders.Domain.Model.Commands
{

    /// <summary>
    /// command to delete all order entry by id.
    /// </summary>
    /// <param name="id">The id identifier</param>
    /// 
    public record DeleteOrdersByIdCommand(int id);
}
