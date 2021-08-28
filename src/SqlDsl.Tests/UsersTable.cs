using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests
{
    public class UsersTable : Table
    {
        public ColumnExpression<int> Id { get; }

        public ColumnExpression<string> Name { get; }

        public ColumnExpression<int> Age { get; }

        public UsersTable(string alias = null) : base("users", alias)
        {
            Id = CreateColumn<int>("id");
            Name = CreateColumn<string>("name");
            Age = CreateColumn<int>("age");
        }
    }
}