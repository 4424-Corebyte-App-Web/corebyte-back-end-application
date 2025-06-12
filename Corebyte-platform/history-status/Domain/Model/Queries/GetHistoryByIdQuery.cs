namespace Corebyte_platform.history_status.Domain.Model.Queries
{
    /// <summary>
    ///     Query to get a history by id.
    /// </summary>
    /// <param name="Id">The id to search</param>
    public record GetHistoryByIdQuery(int Id);
}
