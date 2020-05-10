using FluentMigrator.Builders.Create.Table;

namespace Blog.Persistence.Extensions
{
    internal static class MigrationExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithIdColumn(this ICreateTableWithColumnSyntax tableWithColumnSyntax)
        {
            return tableWithColumnSyntax
                .WithColumn("id")
                .AsInt64()
                .NotNullable()
                .PrimaryKey()
                .Identity();
        }
    }
}
