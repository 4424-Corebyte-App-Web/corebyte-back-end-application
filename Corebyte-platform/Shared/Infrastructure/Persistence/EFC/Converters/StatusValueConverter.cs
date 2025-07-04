using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Corebyte_platform.history_status.Domain.Model.ValueObjects;

namespace Corebyte_platform.Shared.Infrastructure.Persistence.EFC.Converters
{
    public class StatusValueConverter : ValueConverter<Status, string>
    {
        public StatusValueConverter() 
            : base(
                v => v.ToString().ToUpper(),
                v => ConvertToStatus(v),
                new ConverterMappingHints(size: 50))
        {
        }

        private static Status ConvertToStatus(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) 
                return Status.PENDING;
                
            var normalizedValue = value.Trim().ToUpper();
            if (Enum.TryParse<Status>(normalizedValue, true, out var status) && 
                Enum.IsDefined(typeof(Status), status))
            {
                return status;
            }
            
            // Log warning here or handle invalid status as needed
            return Status.PENDING;
        }
    }
}
