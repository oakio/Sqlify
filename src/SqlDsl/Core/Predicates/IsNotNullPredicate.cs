using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class IsNotNullPredicate<T> : Predicate
    {
        private readonly Expression<T> _column;
        

        public IsNotNullPredicate(Expression<T> column)
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