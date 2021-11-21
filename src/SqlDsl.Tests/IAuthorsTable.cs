using SqlDsl.Core;
using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests
{
    [Table("authors")]
    public interface IAuthorsTable : ITable
    {
        [Column("id")]
        ColumnExpression<int> Id { get; }

        [Column("name")]
        ColumnExpression<string> Name { get; }
    }
}