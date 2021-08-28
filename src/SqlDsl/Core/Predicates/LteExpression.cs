using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class LteExpression<T> : ComparisonPredicate<T>
    {
        public LteExpression(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " <= ");
    }
}