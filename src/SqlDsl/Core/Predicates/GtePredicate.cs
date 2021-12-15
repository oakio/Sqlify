using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class GtePredicate<T> : ComparisonPredicate<T>
    {
        public GtePredicate(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " >= ");
    }
}