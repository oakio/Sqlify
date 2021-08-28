using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class BetweenExpression<T> : PredicateExpression
    {
        private readonly ColumnExpression<T> _left;
        private readonly T _from;
        private readonly T _to;

        public BetweenExpression(ColumnExpression<T> left, T from, T to)
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