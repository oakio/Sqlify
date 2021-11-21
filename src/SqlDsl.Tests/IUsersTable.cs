using SqlDsl.Core;
using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests
{
    [Table("users")]
    public interface IUsersTable : ITable
    {
        [Column("id")]
        ColumnExpression<int> Id { get; }

        [Column("name")]
        ColumnExpression<string> Name { get; }

        [Column("age")]
        ColumnExpression<int> Age { get; }
    }
}