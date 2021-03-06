using Sqlify.Core.Expressions;

namespace Sqlify.Core.Predicates
{
    public sealed class NotInSubQueryPredicate<T> : Predicate
    {
        private readonly Column<T> _column;
        private readonly SelectQuery<T> _query;

        public NotInSubQueryPredicate(Column<T> column, SelectQuery<T> query)
        {
            _column = column;
            _query = query;
        }

        public override void Format(ISqlWriter writer)
        {
            _column.Format(writer);
            writer.Append(" NOT IN (");
            _query.Format(writer);
            writer.Append(")");
        }
    }
}