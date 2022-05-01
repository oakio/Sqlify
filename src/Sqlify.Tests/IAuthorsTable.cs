using Sqlify.Core;
using Sqlify.Core.Expressions;

namespace Sqlify.Tests
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