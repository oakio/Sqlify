using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests
{
    public class AuthorsTable : Table
    {
        public ColumnExpression<int> Id { get; }

        public ColumnExpression<string> Name { get; }

        public AuthorsTable(string alias = null) : base("authors", alias)
        {
            Id = CreateColumn<int>("id");
            Name = CreateColumn<string>("name");
        }
    }
}