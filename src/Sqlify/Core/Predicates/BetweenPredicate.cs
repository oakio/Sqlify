using Sqlify.Core.Expressions;

namespace Sqlify.Core.Predicates
{
    public sealed class BetweenPredicate<T> : Predicate
    {
        private readonly Column<T> _left;
        private readonly T _from;
        private readonly T _to;

        public BetweenPredicate(Column<T> left, T from, T to)
        {
            _left = left;
            _from = from;
            _to = to;
        }

        public override void Format(ISqlWriter writer)
        {
            string nameFrom = writer.AddParam(_from);
            string nameTo = writer.AddParam(_to);

            _left.Format(writer);
            writer.Append(" BETWEEN ");
            writer.Append(nameFrom);
            writer.Append(" AND ");
            writer.Append(nameTo);
        }
    }
}