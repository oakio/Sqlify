using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests
{
    public class BooksTable : Table
    {
        public ColumnExpression<int> Id { get; }

        public ColumnExpression<int> AuthorId { get; }

        public ColumnExpression<double> Rating { get; }

        public BooksTable(string alias = null) : base("books", alias)
        {
            Id = CreateColumn<int>("id");
            AuthorId = CreateColumn<int>("author_id");
            Rating = CreateColumn<double>("rating");
        }
    }
}