using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class NeqExpression<T> : ComparisonPredicate<T>
    {
        public NeqExpression(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " <> ");
    }
}