using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Interfaces.REST.Resources;

namespace Corebyte_platform.history_status.Interfaces.REST.Transform
{
    public static class DeleteHistoryCommandFromResourceAssembler
    {
        public static DeleteHistoriesByIdCommand ToCommandFromResource(DeleteHistoryResource resource)
        {
            return new DeleteHistoriesByIdCommand(resource.Id);
        }
    }
}