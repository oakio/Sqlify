using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class IsNullExpression<T> : PredicateExpression
    {
        private readonly Expression<T> _column;

        public IsNullExpression(Expression<T> column)
        {
            _column = column;
        }

        public override void Format(ISqlWriter sql)
        {
            _column.Format(sql);
            sql.Append(" IS NULL");
        }
    }
}