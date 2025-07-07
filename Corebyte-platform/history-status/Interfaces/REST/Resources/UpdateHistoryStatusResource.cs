using Corebyte_platform.history_status.Domain.Model.ValueObjects;

namespace Corebyte_platform.history_status.Interfaces.REST.Resources
{
    public class UpdateHistoryStatusResource
    {
        public Status Status { get; set; }
    }
}
