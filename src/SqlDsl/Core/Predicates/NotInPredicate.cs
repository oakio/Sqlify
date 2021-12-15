using System.Collections.Generic;
using SqlDsl.Core.Expressions;

namespace SqlDsl.Core.Predicates
{
    public sealed class NotInPredicate<T> : Predicate
    {
        private readonly ColumnExpression<T> _left;
        private readonly IReadOnlyCollection<T> _right;

        public NotInPredicate(ColumnExpression<T> left, IReadOnlyCollection<T> right)
        {
            _left = left;
            _right = right;
        }

        public override void Format(ISqlWriter writer)
        {
            string name = writer.AddParam(_right);

            _left.Format(writer);
            writer.Append(" NOT IN ");
            writer.Append(name);
        }
    }
}