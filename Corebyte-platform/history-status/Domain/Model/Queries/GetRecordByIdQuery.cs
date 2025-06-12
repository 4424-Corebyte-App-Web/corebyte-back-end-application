namespace Corebyte_platform.history_status.Domain.Model.Queries
{
    /// <summary>
    ///     Query to get a record by id.
    /// </summary>
    /// <param name="Id">The id to search</param>
    public record GetRecordByIdQuery(int id);
}
