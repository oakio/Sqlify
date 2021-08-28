using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class GtExpression<T> : ComparisonPredicate<T>
    {
        public GtExpression(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " > ");
    }
}