using System;

namespace Corebyte_platform.batch_management.Domain.Model.Queries
{
    /// <summary>
    /// Query for getting a batch by ID
    /// </summary>
    public class GetBatchQuery
    {
        /// <summary>
        /// Constructor for GetBatchQuery
        /// </summary>
        public GetBatchQuery(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
        
        public string Id { get; }
    }
    
    /// <summary>
    /// Query for getting batches with optional filtering
    /// </summary>
    public class GetBatchesQuery
    {
        /// <summary>
        /// Constructor for GetBatchesQuery
        /// </summary>
        public GetBatchesQuery(DateTime? fromDate, DateTime? toDate, string type, string status)
        {
            FromDate = fromDate;
            ToDate = toDate;
            Type = type;
            Status = status;
        }
        
        public DateTime? FromDate { get; }
        public DateTime? ToDate { get; }
        public string Type { get; }
        public string Status { get; }
    }
    
    /// <summary>
    /// Query for getting batches by lot number
    /// </summary>
    public class GetBatchesByLotQuery
    {
        /// <summary>
        /// Constructor for GetBatchesByLotQuery
        /// </summary>
        public GetBatchesByLotQuery(string nlote)
        {
            NLote = nlote ?? throw new ArgumentNullException(nameof(nlote));
        }
        
        public string NLote { get; }
    }
}

