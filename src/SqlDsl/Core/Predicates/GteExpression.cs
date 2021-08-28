using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class GteExpression<T> : ComparisonPredicate<T>
    {
        public GteExpression(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " >= ");
    }
}