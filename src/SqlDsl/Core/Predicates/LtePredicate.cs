using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class LtePredicate<T> : ComparisonPredicate<T>
    {
        public LtePredicate(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " <= ");
    }
}