using SqlDsl.Core;
using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests
{
    [Table("books")]
    public interface IBooksTable : ITable
    {
        [Column("id")]
        ColumnExpression<int> Id { get; }

        [Column("name")]
        ColumnExpression<string> Name { get; }

        [Column("author_id")]
        ColumnExpression<int> AuthorId { get; }

        [Column("rating")]
        ColumnExpression<double> Rating { get; }

        [Column("qty")]
        ColumnExpression<int> Quantity { get; }
    }
}