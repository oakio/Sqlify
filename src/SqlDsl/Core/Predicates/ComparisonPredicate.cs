using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public abstract class ComparisonPredicate<T> : Predicate
    {
        private readonly Expression<T> _left;
        private readonly Expression<T> _right;

        protected ComparisonPredicate(Expression<T> left, Expression<T> right)
        {
            _left = left;
            _right = right;
        }

        protected void Format(ISqlWriter sql, string type)
        {
            _left.Format(sql);
            sql.Append(type);
            _right.Format(sql);
        }
    }
}