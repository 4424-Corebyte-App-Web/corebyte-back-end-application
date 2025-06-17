using Microsoft.EntityFrameworkCore;
using Humanizer;

namespace Corebyte_platform.Shared.Infrastucture.Persistence.EFC.Configuration.Extensions
{
    public static class ModelBuilderExtensions
    {/// <summary>
     ///     Use snake case naming convention
     /// </summary>
     /// <remarks>
     ///     This method sets the naming convention for the database tables, columns, keys, foreign keys and indexes to snake
     ///     case.
     /// </remarks>
        public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (!string.IsNullOrEmpty(tableName)) entity.SetTableName(tableName.Pluralize().Underscore());

                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.GetColumnName().Underscore());

                foreach (var key in entity.GetKeys())
                {
                    var keyName = key.GetName();
                    if (!string.IsNullOrEmpty(keyName)) key.SetName(keyName.Underscore());
                }

                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    var foreignKeyName = foreignKey.GetConstraintName();
                    if (!string.IsNullOrEmpty(foreignKeyName)) foreignKey.SetConstraintName(foreignKeyName.Underscore());
                }

                foreach (var index in entity.GetIndexes())
                {
                    var indexDatabaseName = index.GetDatabaseName();
                    if (!string.IsNullOrEmpty(indexDatabaseName)) index.SetDatabaseName(indexDatabaseName.Underscore());
                }
            }
        }
    }
}
