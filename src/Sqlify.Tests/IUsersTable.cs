using Sqlify.Core;
using Sqlify.Core.Expressions;

namespace Sqlify.Tests
{
    [Table("users")]
    public interface IUsersTable : ITable
    {
        [Column("id" )]
        Column<int> Id { get; }

        [Column("name")]
        Column<string> Name { get; }

        [Column("last_name", "last_name::varchar")]
        Column<string> LastName { get; }

        [Column("age")]
        Column<int> Age { get; }
    }
}