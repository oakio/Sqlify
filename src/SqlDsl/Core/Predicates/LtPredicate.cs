using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class LtPredicate<T> : ComparisonPredicate<T>
    {
        public LtPredicate(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " < ");
    }
}