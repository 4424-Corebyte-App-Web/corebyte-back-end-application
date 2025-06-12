namespace Corebyte_platform.history_status.Domain.Model.Commands
{
    /// <summary>
    /// Command to delete all recods for a specific id
    /// </summary>
    /// <param name="Id">The id identifier</param>
    public record DeleteRecordByIdCommand(int Id);
}
