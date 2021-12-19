using SqlDsl.Core;
using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests
{
    [Table("authors")]
    public interface IAuthorsTable : ITable
    {
        [Column("id")]
        Column<int> Id { get; }

        [Column("name")]
        Column<string> Name { get; }
    }
}