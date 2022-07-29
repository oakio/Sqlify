using Sqlify.Core;
using Sqlify.Core.Expressions;

namespace Sqlify.Tests
{
    [Table("users")]
    public interface IUsersTable : ITable
    {
        [Column("id", "id::varchar")]
        Column<int> Id { get; }

        [Column("name")]
        Column<string> Name { get; }

        [Column("age")]
        Column<int> Age { get; }
    }
}