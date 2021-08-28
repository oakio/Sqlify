using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class NotInSubQueryExpression<T> : PredicateExpression
    {
        private readonly ColumnExpression<T> _column;
        private readonly SelectSqlQuery<T> _query;

        public NotInSubQueryExpression(ColumnExpression<T> column, SelectSqlQuery<T> query)
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