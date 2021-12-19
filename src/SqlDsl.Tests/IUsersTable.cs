using SqlDsl.Core;
using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests
{
    [Table("users")]
    public interface IUsersTable : ITable
    {
        [Column("id")]
        Column<int> Id { get; }

        [Column("name")]
        Column<string> Name { get; }

        [Column("age")]
        Column<int> Age { get; }
    }
}