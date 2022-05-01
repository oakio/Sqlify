using Sqlify.Core.Expressions;

namespace Sqlify.Core.Predicates
{
    public sealed class EqPredicate<T> : ComparisonPredicate<T>
    {
        public EqPredicate(Expression<T> left, Expression<T> right) : base(left, right)
        {
        }

        public override void Format(ISqlWriter sql) => Format(sql, " = ");
    }
}