namespace Corebyte_platform.history_status.Domain.Model.Commands
{
    /// <summary>
    /// Command to delete all history records for a specific id
    /// </summary>
    /// <param name="id">The id identifier</param>
    public record DeleteHistoriesByIdCommand(int id);
}