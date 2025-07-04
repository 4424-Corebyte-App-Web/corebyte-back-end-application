namespace Corebyte_platform.history_status.Domain.Model.Queries
{
    /// <summary>
    ///     Query to get a history by status.
    /// </summary>
    /// <param name="status">The status to search</param>
    public record GetHistoryByStatusQuery(String status);
}