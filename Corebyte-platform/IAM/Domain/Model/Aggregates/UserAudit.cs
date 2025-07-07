using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Corebyte_platform.IAM.Domain.Model.Aggregates;

public partial class User : IEntityWithCreatedUpdatedDate
{
    [Column(name:"CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column(name:"UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}