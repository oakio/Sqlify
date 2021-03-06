using Sqlify.Core.Expressions;

namespace Sqlify.Core.Predicates
{
    public sealed class GtPredicate<T> : ComparisonPredicate<T>
    {
        public GtPredicate(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " > ");
    }
}