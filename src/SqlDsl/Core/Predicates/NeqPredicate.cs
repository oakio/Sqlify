using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class NeqPredicate<T> : ComparisonPredicate<T>
    {
        public NeqPredicate(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " <> ");
    }
}