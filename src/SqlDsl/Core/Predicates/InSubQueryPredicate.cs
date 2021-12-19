using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class InSubQueryPredicate<T> : Predicate
    {
        private readonly Column<T> _column;
        private readonly SelectQuery<T> _query;

        public InSubQueryPredicate(Column<T> column, SelectQuery<T> query)
        {
            _column = column;
            _query = query;
        }

        public override void Format(ISqlWriter writer)
        {
            _column.Format(writer);
            writer.Append(" IN (");
            _query.Format(writer);
            writer.Append(")");
        }
    }
}