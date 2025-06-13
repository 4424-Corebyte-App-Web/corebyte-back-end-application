using Corebyte_platform.history_status.Domain.Model.Commands;
using Corebyte_platform.history_status.Interfaces.REST.Resources;

namespace Corebyte_platform.history_status.Interfaces.REST.Transform
{
    public static class DeleteRecordCommandFromResourceAssembler
    {
        public static DeleteRecordByIdCommand ToCommandFromResources(DeleteRecordResource resource) { 
            return new DeleteRecordByIdCommand(resource.Id);
        }
    }
}
