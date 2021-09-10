using SqlDsl.Core.Expressions;

namespace SqlDsl.Tests
{
    public class BooksTable : Table
    {
        public ColumnExpression<int> Id { get; }

        public ColumnExpression<string> Name { get; }

        public ColumnExpression<int> AuthorId { get; }

        public ColumnExpression<double> Rating { get; }

        public ColumnExpression<int> Quantity { get; }

        public BooksTable(string alias = null) : base("books", alias)
        {
            Id = CreateColumn<int>("id");
            Name = CreateColumn<string>("name");
            AuthorId = CreateColumn<int>("author_id");
            Rating = CreateColumn<double>("rating");
            Quantity = CreateColumn<int>("qty");
        }
    }
}