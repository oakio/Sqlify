using Sqlify.Core;
using Sqlify.Core.Expressions;

namespace Sqlify.Tests
{
    [Table("books")]
    public interface IBooksTable : ITable
    {
        [Column("id")]
        Column<int> Id { get; }

        [Column("name")]
        Column<string> Name { get; }

        [Column("author_id")]
        Column<int> AuthorId { get; }

        [Column("rating")]
        Column<double> Rating { get; }

        [Column("qty")]
        Column<int> Quantity { get; }
    }
}