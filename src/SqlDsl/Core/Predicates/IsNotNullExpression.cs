using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class IsNotNullExpression<T> : PredicateExpression
    {
        private readonly Expression<T> _column;
        

        public IsNotNullExpression(Expression<T> column)
        {
            _column = column;
        }

        public override void Format(ISqlWriter sql)
        {
            _column.Format(sql);
            sql.Append(" IS NOT NULL");
        }
    }
}