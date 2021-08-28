using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class LtExpression<T> : ComparisonPredicate<T>
    {
        public LtExpression(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " < ");
    }
}