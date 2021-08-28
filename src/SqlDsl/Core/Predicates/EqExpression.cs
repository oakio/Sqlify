using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class EqExpression<T> : ComparisonPredicate<T>
    {
        public EqExpression(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " = ");
    }
}